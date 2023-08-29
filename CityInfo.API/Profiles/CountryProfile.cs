using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Entities.Country, Models.CountryDto>();
            CreateMap<Models.CountryForCreationDto, Entities.Country>();
        }

    }
}
