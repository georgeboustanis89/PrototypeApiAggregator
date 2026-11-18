using PrototypeApiAggregator.Services;
using System.Net.Http.Headers;

namespace PrototypeApiAggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            //HttpService registration
            builder.Services.AddSingleton<HttpService>();

            //Http Client for Posts service
            builder.Services.AddHttpClient<PostsService>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                client.Timeout = TimeSpan.FromSeconds(10);
            });

            //Http Client for Music service
            builder.Services.AddHttpClient<MusicService>(client =>
            {
                client.BaseAddress = new Uri("http://ws.audioscrobbler.com/2.0/");
                client.Timeout = TimeSpan.FromSeconds(15);
            });

            //http client for Country service
            builder.Services.AddHttpClient<CountryService>(client =>
            {
                client.BaseAddress = new Uri("https://api.restcountries.com/countries/v5/names.common/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_API_KEY");
                client.Timeout = TimeSpan.FromSeconds(15);
            });

            //Aggregate service registration
            builder.Services.AddScoped<AggregateService>();

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
