using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MemberPro.Core.Models.Media;
using MemberPro.Core.Security;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("attachments")]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IWorkContext _workContext;
        private readonly ILogger<AttachmentsController> _logger;

        public AttachmentsController(IAttachmentService attachmentService,
            IWorkContext workContext,
            ILogger<AttachmentsController> logger)
        {
            _attachmentService = attachmentService;
            _workContext = workContext;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttachmentModel>> GetById(int id)
        {
            var attachment = await _attachmentService.FindByIdAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            return Ok(attachment);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AttachmentModel>>> GetAll()
        {
            var attachments = await _attachmentService.GetAll(_workContext.GetCurrentUserId());

            return Ok(attachments);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AttachmentModel>>> Search(string objectType, int objectId)
        {
            var attachments = await _attachmentService.GetByObjectAsync(objectType, objectId, _workContext.GetCurrentUserId());

            return Ok(attachments);
        }

        [HttpPost("")]
        public async Task<ActionResult<AttachmentModel>> Create([FromForm] UploadAttachmentModel model)
        {
            model.OwnerId = _workContext.GetCurrentUserId();
            model.FileName = model.File.FileName;
            model.ContentType = model.File.ContentType;

            try
            {
                byte[] fileBytes;
                using (var memStream = new MemoryStream())
                {
                    await model.File.CopyToAsync(memStream);
                    fileBytes = memStream.ToArray();
                }

                var result = await _attachmentService.CreateAsync(model, fileBytes);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }

    public class UploadAttachmentModel : CreateAttachmentModel
    {
        public IFormFile File { get; set; }
    }
}
