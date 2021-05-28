using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Models.Geography;
using Microsoft.EntityFrameworkCore;

namespace MemberPro.Core.Services.Geography
{
    public interface IStateProvinceService
    {
        Task<StateProvinceModel> FindByIdAsync(int id);

        Task<IEnumerable<StateProvinceModel>> GetAllAsync();
        Task<IEnumerable<StateProvinceModel>> GetByCountryIdAsync(int countryId);
    }

    public class StateProvinceService : IStateProvinceService
    {
        private readonly IRepository<StateProvince> _repository;
        private readonly IMapper _mapper;

        public StateProvinceService(IRepository<StateProvince> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StateProvinceModel> FindByIdAsync(int id)
        {
            var state = await _repository.GetByIdAsync(id);
            if (state == null)
                return null;

            return _mapper.Map<StateProvinceModel>(state);
        }

        public async Task<IEnumerable<StateProvinceModel>> GetAllAsync()
        {
            var states = await _repository.TableNoTracking
                .OrderBy(x => x.Name)
                .ProjectTo<StateProvinceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return states;
        }

        public async Task<IEnumerable<StateProvinceModel>> GetByCountryIdAsync(int countryId)
        {
            var states = await _repository.TableNoTracking
                .Where(x => x.CountryId == countryId)
                .Include(x => x.Country)
                .OrderBy(x => x.Name)
                .ProjectTo<StateProvinceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return states;
        }
    }
}
