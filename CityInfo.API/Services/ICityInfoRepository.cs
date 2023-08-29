using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country?> GetCountryAsync(int countryId);
        Task<bool> CountryExistsAsync(int countryId);

        void DeleteCountry(Country country);

        Task AddCountry(Country country);

        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? seachQuery, int pageNumber, int pageSize);

        Task<City?> GetCityForCountryAsync(int countryId, int cityId);
        Task<IEnumerable<City>> GetCitiesForCountryAsync(int countryId);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
