# Prototype API Aggregator

This is a small ASP.NET Core Web API project created as part of a coding assessment. The idea is to retrieve data from different external APIs and return them in a single response.

## APIs used

- JSONPlaceholder - retrieves a post by its ID.
- Last.fm - retrieves top tracks for a given country.
- REST Countries - retrieves country information such as population, capital and region.

## How it works

There is a separate service for each external API and a common `HttpService` for HTTP GET requests, JSON deserialization and basic error handling.

The `AggregateService` calls the three services concurrently using `Task.WhenAll` and combines their results. Each result contains its own success flag, data, error message and status code (when available). This allows the aggregator to return partial results when an external API returns a handled error.

## Endpoint

The controller exposes one GET endpoint:

```http
GET /all/{country}/{id}
```

For example:

```http
GET /all/greece/1
```

This returns the country information and top tracks for Greece, along with the post with ID 1 from JSONPlaceholder. Both parameters are passed to the `AggregateService`.

## Caching

I used `IMemoryCache` to avoid unnecessary requests to the external APIs. Each service has its own cache keys and expiration time:

- Posts: 20 minutes
- Music: 10 minutes
- Country information: 1 minute

The cache is stored in memory and is cleared when the application restarts.

## Running the project

The project uses .NET 10. Before running it, replace the API key placeholders in the source code with the keys provided separately by email.

Open the solution in Visual Studio or navigate to the directory containing the `.csproj` file and run:

```bash
dotnet restore
dotnet run
```

Use the local address shown in the console to try the endpoint above.

For this assessment, I decided to send the API keys separately by email so that reviewers do not need to obtain their own keys. I am aware that keeping API keys directly in the source code is not a secure practice. In a real application, I would use User Secrets for local development or environment variables instead.

## Things that are not finished yet

- API statistics and response time monitoring.
- Unit tests.
- Some additional response validation and error handling improvements.

The main aggregation and in-memory caching functionality is implemented, but there is still room for improvement.
