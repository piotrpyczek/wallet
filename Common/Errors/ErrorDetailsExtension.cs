using System.Text.Json;

namespace Common;

public static class ErrorDetailsExtension
{
    public static async Task<IErrorDetails> GetErrorDetailsAsync(this HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var exhangeRateResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ErrorDetails>(exhangeRateResponse, JsonDefaults.CaseInsensitiveOptions)!;
    }
}