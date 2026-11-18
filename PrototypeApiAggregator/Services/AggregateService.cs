using Microsoft.AspNetCore.Http.HttpResults;
using PrototypeApiAggregator.Models;
using System.Net;
using System.Text.Json;

namespace PrototypeApiAggregator.Services
{
    public class AggregateService
    {
        private readonly PostsService _postsService;
        private readonly MusicService _musicService;
        private readonly CountryService _countryService;

        public AggregateService(PostsService postsService, MusicService musicService, CountryService countryService)
        {
            _postsService = postsService;
            _musicService = musicService;
            _countryService = countryService;

        }

        public async Task<AggregatedResponse> GetAll(int id, string country, CancellationToken ct)
        {
            var postResponseTask = _postsService.GetPostAsync(id, ct);
            var musicResponseTask = _musicService.GetMusicDataByCountry(country, ct);
            var countryResponseTask = _countryService.GetCountryInfo(country, ct);

            await Task.WhenAll(
                postResponseTask,
                musicResponseTask,
                countryResponseTask);

            return new()
            {
                Country = country,
                Post = await postResponseTask,
                TopTracks = await musicResponseTask,
                CountryInfo = await countryResponseTask
            };
        }

        public async Task<ResponseResult<PostDto>> GetPostAsync(int id, CancellationToken ct)
            => await _postsService.GetPostAsync(id,ct);

        public async Task<ResponseResult<MusicResponseDto>> GetMusicDataAsync(string country, CancellationToken ct)
            => await _musicService.GetMusicDataByCountry(country,ct);
    }
}
