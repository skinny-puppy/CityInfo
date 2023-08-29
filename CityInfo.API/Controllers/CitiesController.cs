using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/countries/{countryId}/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PointsOfInterestController> _logger;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper, ILogger<PointsOfInterestController> logger) 
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(
            int countryId)
        {
            if (!await _cityInfoRepository.CountryExistsAsync(countryId))
            {
                _logger.LogInformation(
                    $"Country with id {countryId} wasn't found when accessing city.");
                return NotFound();
            }

            var cityInCountry = await _cityInfoRepository
                .GetCitiesForCountryAsync(countryId);

            return Ok(_mapper.Map<IEnumerable<CityDto>>(cityInCountry));
        }

        [HttpGet("{cityid}", Name = "GetCity")]
        public async Task<ActionResult<CityDto>> GetCity(
            int countryId, int cityId)
        {
            if (!await _cityInfoRepository.CountryExistsAsync(countryId))
            {
                _logger.LogInformation(
                    $"Country with id {countryId} wasn't found when accessing city.");
                return NotFound();
            }

            var city = await _cityInfoRepository
                .GetCityForCountryAsync(countryId, cityId);

            if (city == null)
            {
                _logger.LogInformation(
                    $"City with id {cityId} wasn't found, but country exists.");
                return NotFound();
            }

            return Ok(_mapper.Map<CityDto>(city));
        }

        // stara metoda sa paginacijom, bez provere za country
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        { 


            if (pageSize > maxCitiesPageSize)
            {
                pageSize= maxCitiesPageSize;
            }


            var (cityEntities, paginationMetadata) = await _cityInfoRepository
                .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            //dodavanje headera sa pagination metadata
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CityDto>>(cityEntities));

            // RUCNO MAPIRANJE
            var results = new List<CityDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityDto()
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }



        }*/

    }
}
