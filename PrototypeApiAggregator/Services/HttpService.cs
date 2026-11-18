using PrototypeApiAggregator.Models;
using System.Text.Json;

namespace PrototypeApiAggregator.Services
{
    public class HttpService
    {
        public async Task<ResponseResult<T>> GetAsync<T>(HttpClient httpClient, string endpoint, CancellationToken ct) where T : class
        {
            int? responseStatusCode = null;

            try
            {
                using var response = await httpClient.GetAsync(endpoint, ct);

                responseStatusCode = (int)response.StatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    return ResponseResult<T>.Fail($"External API error: {responseStatusCode}", responseStatusCode);
                }

                T? data = await response.Content.ReadFromJsonAsync<T>(ct);

                if (data == null)
                {
                    return ResponseResult<T>.Fail("Empty Response", responseStatusCode);
                }

                return ResponseResult<T>.Ok(data, responseStatusCode);
            }
            catch (HttpRequestException)
            {
                return ResponseResult<T>.Fail("Communication Error");
            }
            catch (OperationCanceledException) when (!ct.IsCancellationRequested)
            {
                return ResponseResult<T>.Fail("Request Timeout");
            }
            catch (JsonException)
            {
                return ResponseResult<T>.Fail("Invalid JSON", responseStatusCode);
            }
        } 
    }
}
