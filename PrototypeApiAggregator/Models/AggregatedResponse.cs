namespace PrototypeApiAggregator.Models
{
    public class AggregatedResponse
    {
        public string Country { get; init; } = string.Empty;
        public ResponseResult<PostDto> Post { get; init; }
        public ResponseResult<MusicResponseDto> TopTracks { get; init; }
        public ResponseResult<CountryResponseDto> CountryInfo { get; init; }
    }
}
