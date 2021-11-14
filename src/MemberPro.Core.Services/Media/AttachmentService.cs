using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Media;
using MemberPro.Core.Enums;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Media;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Media
{
    public interface IAttachmentService
    {
        Task<AttachmentModel> FindByIdAsync(int id);

        Task<IEnumerable<AttachmentModel>> GetAll(int userId);
        Task<IEnumerable<AttachmentModel>> GetByObjectAsync(string objectType, int objectId, int userId);

        Task<AttachmentModel> CreateAsync(CreateAttachmentModel model, byte[] fileBytes);
        Task DeleteAsync(int id);
    }

    public class AttachmentService : IAttachmentService
    {
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMediaHelper _mediaHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<AttachmentService> _logger;

        private readonly string[] _imageSizeKeys = new[] { "square_150", "thumb", "small_240", "medium_640", "large_1024", "large_1600" };

        public AttachmentService(IRepository<Attachment> attachmentRepository,
            IDateTimeService dateTimeService,
            IFileStorageService fileStorageService,
            IMediaHelper mediaHelper,
            IMapper mapper,
            ILogger<AttachmentService> logger)
        {
            _attachmentRepository = attachmentRepository;
            _dateTimeService = dateTimeService;
            _fileStorageService = fileStorageService;
            _mediaHelper = mediaHelper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AttachmentModel> FindByIdAsync(int id)
        {
            var attachment = await _attachmentRepository.TableNoTracking.SingleOrDefaultAsync(x => x.Id == id);
            if (attachment == null)
                return null;

            var model = _mapper.Map<AttachmentModel>(attachment);

            var path = GetFilePath(attachment.OwnerId, attachment.ObjectType, attachment.FileName);

            model.Url = _fileStorageService.ResolveFileUrl(path);
            return model;
        }

        public async Task<IEnumerable<AttachmentModel>> GetAll(int userId)
        {
            var attachments = await _attachmentRepository.TableNoTracking
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<AttachmentModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach(var attachment in attachments)
            {
                var path = GetFilePath(userId, attachment.ObjectType, attachment.FileName);
                attachment.Url = _fileStorageService.ResolveFileUrl(path);

                if (attachment.MediaType == AttachmentType.Photo)
                {
                    foreach (var sizeKey in _imageSizeKeys)
                    {
                        var fileName = _mediaHelper.GetResizedImageName(attachment.SaveFileName, sizeKey);
                        var filePath = GetFilePath(userId, attachment.ObjectType, fileName);
                        attachment.VariantUrls.Add(sizeKey, _fileStorageService.ResolveFileUrl(filePath));
                    }
                }
            }

            return attachments;
        }

        public async Task<IEnumerable<AttachmentModel>> GetByObjectAsync(string objectType, int objectId, int userId)
        {
            var attachments = await _attachmentRepository.TableNoTracking
                .Where(x => x.ObjectType == objectType
                    && x.ObjectId == objectId
                    && x.OwnerId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<AttachmentModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            foreach(var attachment in attachments)
            {
                var path = GetFilePath(userId, attachment.ObjectType, attachment.SaveFileName);
                attachment.Url = _fileStorageService.ResolveFileUrl(path);

                if (attachment.MediaType == AttachmentType.Photo)
                {
                    foreach (var sizeKey in _imageSizeKeys)
                    {
                        var fileName = _mediaHelper.GetResizedImageName(attachment.SaveFileName, sizeKey);
                        var filePath = GetFilePath(userId, attachment.ObjectType, fileName);
                        attachment.VariantUrls.Add(sizeKey, _fileStorageService.ResolveFileUrl(filePath));
                    }
                }
            }

            return attachments;
        }

        public async Task<AttachmentModel> CreateAsync(CreateAttachmentModel model, byte[] fileBytes)
        {
            try
            {
                // TODO: Need to get content type, probabl shouldn't assume it's being passed in
                // TODO: (nice to have) if document, can we somehow have a thumbnail

                var saveFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.FileName)}";
                var savePath = GetFilePath(model.OwnerId, model.ObjectType, saveFileName);

                using (var memStream = new MemoryStream(fileBytes))
                {
                    await _fileStorageService.SaveFileAsync(savePath, model.ContentType, memStream);
                }

                var mediaType = _mediaHelper.GetMediaTypeFromContentType(model.ContentType);

                var attachment = new Attachment
                {
                    OwnerId = model.OwnerId,
                    ObjectType = model.ObjectType,
                    ObjectId = model.ObjectId,
                    MediaType = mediaType,
                    FileName = model.FileName,
                    SaveFileName = saveFileName,
                    FileSize = model.FileSize,
                    ContentType = model.ContentType,
                    CreatedOn = _dateTimeService.NowUtc,
                };

                await _attachmentRepository.CreateAsync(attachment);

                var result = await FindByIdAsync(attachment.Id);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error saving attachment record");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var attachmentEntity = await _attachmentRepository.GetByIdAsync(id);
            if (attachmentEntity is null)
            {
                throw new ItemNotFoundException($"Attachment ID {id} not found");
            }

            _logger.LogInformation($"Deleting attachment ID {id}");

            try
            {
                // Delete the record first...
                await _attachmentRepository.DeleteAsync(attachmentEntity);

                _logger.LogDebug($"Deleted attachment record ID {id}");

                // Delete the original
                var filePath = GetFilePath(attachmentEntity.OwnerId, attachmentEntity.ObjectType, attachmentEntity.SaveFileName);
                await _fileStorageService.DeleteFileAsync(filePath);

                _logger.LogDebug($"Deleted file {attachmentEntity.SaveFileName} (attachment ID {id})");

                // If image, delete all the resizes...
                if (attachmentEntity.MediaType == AttachmentType.Photo)
                {
                    _logger.LogDebug($"Attachment ID {id} is Photo... deleting resized images...");

                    foreach(var imgSize in _imageSizeKeys)
                    {
                        var sizeFileName = _mediaHelper.GetResizedImageName(attachmentEntity.SaveFileName, imgSize);
                        var sizePath = GetFilePath(attachmentEntity.OwnerId, attachmentEntity.ObjectType, sizeFileName);

                        _logger.LogDebug($"...deleting file {sizePath} (attachment ID {id})");

                        await _fileStorageService.DeleteFileAsync(sizePath);

                        _logger.LogDebug($"... deleted file {sizePath} (attachment ID {id})");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error deleting attachment {id}");
                throw;
            }
        }

        protected string GetFilePath(int userId, string objType, string fileName)
            => $"users/{userId}/{objType.ToLower()}/{fileName}";
    }
}
