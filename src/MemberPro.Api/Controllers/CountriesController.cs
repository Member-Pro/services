using System.Collections.Generic;
using System.Threading.Tasks;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Geography;
using MemberPro.Core.Models.Geography;
using MemberPro.Core.Services.Geography;
using Microsoft.AspNetCore.Mvc;

namespace MemberPro.Api.Controllers
{
    [ApiController]
    [Route("countries")]
    public class CountriesController : ControllerBase
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly ICountryService _countryService;

        public CountriesController(IRepository<Country> countryRepository,
            ICountryService countryService)
        {
            _countryRepository = countryRepository;
            _countryService = countryService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CountryModel>>> Get()
        {
            var countries = await _countryService.GetAllAsync();

            return Ok(countries);
        }

        //[HttpPost("")]
        //public async Task<ActionResult> Create(CreateCountryModel model)
        //{
        //    var country = new Country
        //    {
        //        Name = model.Name,
        //        Abbreviation = model.Abbreviation
        //    };

        //    await _countryRepository.CreateAsync(country);

        //    return Ok();
        //}
    }
}
