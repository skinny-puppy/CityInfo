using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public CountriesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper, IMailService mailService)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countryEntities = await _cityInfoRepository.GetCountriesAsync();
            return Ok(_mapper.Map<IEnumerable<CountryDto>>(countryEntities));
        }

        [HttpGet("{id}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(
            int id)
        {
            var country = await _cityInfoRepository.GetCountryAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryDto>(country));
        }
        
        [HttpPost] 
        public async Task<ActionResult<CountryDto>> CreateCountry(
           CountryForCreationDto country)
        {
            var finalCountry = _mapper.Map<Entities.Country>(country);

            await _cityInfoRepository.AddCountry(finalCountry);

            await _cityInfoRepository.SaveChangesAsync();

            var createdCountryToReturn =
                _mapper.Map<Models.CountryDto>(finalCountry);

            return CreatedAtRoute("GetCountry",
                 new
                 {
                     id = createdCountryToReturn.Id
                 },
                 createdCountryToReturn);
        }


        [HttpDelete("{countryId}")]
        public async Task<ActionResult> DeleteCountry(
            int countryId)
        {
            if (!await _cityInfoRepository.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var countryEntity = await _cityInfoRepository
                .GetCountryAsync(countryId);
            if (countryEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeleteCountry(countryEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send(
                "Country deleted.",
                $"Country {countryEntity.Name} with id {countryEntity.Id} was deleted.");

            return NoContent();
        }
    }

}
