using Microsoft.Extensions.Caching.Memory;
using PrototypeApiAggregator.Models;

namespace PrototypeApiAggregator.Services
{
    public class CountryService
    {
        private readonly HttpService _httpService;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public CountryService(HttpService httpService, HttpClient httpClient, IMemoryCache cache)
        {
            _httpService = httpService;
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<ResponseResult<CountryResponseDto>> GetCountryInfo(string country, CancellationToken ct)
        {
            //e.g. "greece" same with "GrEece"
            var cacheKey = $"country:{country.Trim().ToLowerInvariant()}";

            if (_cache.TryGetValue<ResponseResult<CountryResponseDto>>(cacheKey, out var cachedResponse) && cachedResponse != null)
                return cachedResponse;

            var response = await _httpService.GetAsync<CountryResponseDto>(_httpClient, $"https://api.restcountries.com/countries/v5/names.common/{country}", ct);
            if (response?.Data?.Data.Objects is not { Count: > 0}) //property pattern matching
                return ResponseResult<CountryResponseDto>.Fail("Unknown error from country service");

            _cache.Set(cacheKey,response,TimeSpan.FromMinutes(1));
            return response;

        }
    }
}
