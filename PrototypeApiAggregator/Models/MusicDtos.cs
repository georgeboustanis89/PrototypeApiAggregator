namespace PrototypeApiAggregator.Models
{
    public class MusicResponseDto
    {
        public MusicDataDto? Tracks { get; set; }
        public int? Error { get; set; }
        public string? Message { get; set; }
    }

    public class MusicDataDto
    {
        public List<MusicTrackDto> Track { get; set; }
    }

    public class MusicTrackDto
    {
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public MusicArtistDto? Artist { get; set; }
    }

    public class MusicArtistDto
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}
