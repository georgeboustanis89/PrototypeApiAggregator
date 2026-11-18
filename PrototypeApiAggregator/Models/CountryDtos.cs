namespace PrototypeApiAggregator.Models
{
    public class CountryResponseDto
    {
        public CountryDataDto Data { get; set; }
    }

    public class CountryDataDto
    {
        public List<CountryInfoDto> Objects { get; set; }
    }

    public class CountryInfoDto
    {
        public List<CountryCapitalDto> Capitals { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public int Population { get; set; }
        public CountryFlagDto Flag { get; set; }
    }

    public class CountryCapitalDto
    {
        public string Name { get; set; }
    }
    public class CountryFlagDto
    {
        public string Emoji { get; set; }
    }
}
