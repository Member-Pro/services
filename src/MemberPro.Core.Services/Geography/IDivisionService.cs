using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Models.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Geography
{
    public interface IDivisionService
    {
        Task<DivisionModel> FindById(int id);

        Task<IEnumerable<DivisionModel>> GetByRegionId(int regionId);

        Task<DivisionModel> Create(CreateDivisionModel model);
    }

    public class DivisionService : IDivisionService
    {
        private readonly IRepository<Division> _DivisionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DivisionService> _logger;

        public DivisionService(IRepository<Division> DivisionRepository,
            IMapper mapper,
            ILogger<DivisionService> logger)
        {
            _DivisionRepository = DivisionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DivisionModel> FindById(int id)
        {
            var division = await _DivisionRepository.TableNoTracking
                .Include(x => x.Region)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (division == null)
                return null;

            return _mapper.Map<DivisionModel>(division);
        }

        public async Task<IEnumerable<DivisionModel>> GetByRegionId(int regionId)
        {
            var divisions = await _DivisionRepository.TableNoTracking
                .Include(x => x.Region)
                .Where(x => x.RegionId == regionId)
                .OrderBy(x => x.Name)
                .ProjectTo<DivisionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return divisions;
        }

        public async Task<DivisionModel> Create(CreateDivisionModel model)
        {
            try
            {
                var division = new Division
                {
                    RegionId = model.RegionId, // TODO: Validate me
                    Name = model.Name,
                    Abbreviation = model.Abbreviation,
                };

                await _DivisionRepository.CreateAsync(division);

                return await FindById(division.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating division.");
                throw;
            }
        }
    }
}
