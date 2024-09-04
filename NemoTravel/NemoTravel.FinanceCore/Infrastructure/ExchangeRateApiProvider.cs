using System.Text.Json;

namespace NemoTravel.FinanceCore.Infrastructure;

public class ExchangeRateApiProvider : IExchangeRateProvider
{
    private const string ApiKey = "ca26ef9aab700556198f529b";
    private const string BaseUrl = "https://v6.exchangerate-api.com/v6/";

    private readonly HttpClient _httpClient;

    public ExchangeRateApiProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal?> GetExchangeRate(string fromCurrency, string toCurrency)
    {
        if (fromCurrency == toCurrency)
        {
            return 1;
        }

        var url = $"{BaseUrl}{ApiKey}/latest/{fromCurrency}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }

        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = document.RootElement;

        if (root.TryGetProperty("conversion_rates", out var rates) &&
            rates.TryGetProperty(toCurrency, out var rateElement))
        {
            return rateElement.GetDecimal();
        }

        return null;
    }
}