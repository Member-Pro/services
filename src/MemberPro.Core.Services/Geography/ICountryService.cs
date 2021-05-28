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
    public interface ICountryService
    {
        Task<CountryModel> FindByIdAsync(int id);

        Task<IEnumerable<CountryModel>> GetAllAsync();        
    }

    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _repository;
        private readonly IMapper _mapper;

        public CountryService(IRepository<Country> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CountryModel> FindByIdAsync(int id)
        {
            var country = await _repository.GetByIdAsync(id);
            if (country == null)
                return null;

            return _mapper.Map<CountryModel>(country);
        }

        public async Task<IEnumerable<CountryModel>> GetAllAsync()
        {
            var countries = await _repository.TableNoTracking
                .OrderBy(x => x.Name)
                .ProjectTo<CountryModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return countries;
        }

    }
}
