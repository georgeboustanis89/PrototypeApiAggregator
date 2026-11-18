using Microsoft.Extensions.Caching.Memory;
using PrototypeApiAggregator.Models;
using System.Text.Json;

namespace PrototypeApiAggregator.Services
{
    public class PostsService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpService _httpService;
        private readonly IMemoryCache _cache;

        public PostsService(HttpClient httpClient, HttpService httpService, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _httpService = httpService;
            _cache = cache;
        }

        public async Task<ResponseResult<PostDto>> GetPostAsync(int id, CancellationToken ct)
        {
            var cacheKey = $"posts:{id}";
            if (_cache.TryGetValue<ResponseResult<PostDto>>(cacheKey, out var cachedResponse) && cachedResponse != null)
                return cachedResponse;

            var response = await _httpService.GetAsync<PostDto>(_httpClient,$"posts/{id}", ct);

            if(response.Success && response.Data != null)
                _cache.Set(cacheKey,response,TimeSpan.FromMinutes(20));

            return response;
        }
    }
}
