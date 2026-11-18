using Microsoft.Extensions.Caching.Memory;
using PrototypeApiAggregator.Models;

namespace PrototypeApiAggregator.Services
{
    public class MusicService
    {
        private readonly HttpService _httpService;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string API_KEY = "YOUR_API_KEY";
        public MusicService(HttpService httpService, HttpClient httpClient, IMemoryCache cache)
        {
            _httpService = httpService;
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<ResponseResult<MusicResponseDto>> GetMusicDataByCountry(string country, CancellationToken ct)
        {
            var cacheKey = $"music:{country.Trim().ToLowerInvariant()}";

            if (_cache.TryGetValue<ResponseResult<MusicResponseDto>>(cacheKey, out var cachedResponse) && cachedResponse != null)
                return cachedResponse;

            var response = await _httpService.GetAsync<MusicResponseDto>(_httpClient, $"?method=geo.gettoptracks&country={country}&api_key={API_KEY}&format=json", ct);

            if (!response.Success)
                return response;

            if (response.Data?.Error != null)
                 return ResponseResult<MusicResponseDto>.Fail(
                    response.Data.Message ?? "Unknown error from music service",
                    response.StatusCode);

            if (response.Data?.Tracks?.Track is null)
                return ResponseResult<MusicResponseDto>.Fail(
                    "Invalid music data",
                    response.StatusCode);

            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));

            return response;
        }
    }
}
