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
    public interface IRegionService
    {
        Task<RegionModel> FindById(int id);

        Task<IEnumerable<RegionModel>> GetAll();

        Task<RegionModel> Create(CreateRegionModel model);
    }

    public class RegionService : IRegionService
    {
        private readonly IRepository<Region> _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionService> _logger;

        public RegionService(IRepository<Region> regionRepository,
            IMapper mapper,
            ILogger<RegionService> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RegionModel> FindById(int id)
        {
            var region = await _regionRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
                return null;

            return _mapper.Map<RegionModel>(region);
        }

        public async Task<IEnumerable<RegionModel>> GetAll()
        {
            var regions = await _regionRepository.TableNoTracking
                .OrderBy(x => x.Name)
                .ProjectTo<RegionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return regions;
        }

        public async Task<RegionModel> Create(CreateRegionModel model)
        {
            try
            {
                var region = new Region
                {
                    Name = model.Name,
                    Abbreviation = model.Abbreviation,
                };

                await _regionRepository.CreateAsync(region);

                return await FindById(region.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating region.");
                throw;
            }
        }
    }
}
