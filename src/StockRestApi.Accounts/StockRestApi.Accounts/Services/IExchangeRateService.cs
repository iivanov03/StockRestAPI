public interface IExchangeRateService
{
    Task<double> GetExchangeRate(string fromCurrency, string toCurrency);
}

public class ExchangeRateService : IExchangeRateService
{
    public async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
    {
        string apiUrl = $"https://open.er-api.com/v6/latest/{fromCurrency}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                ExchangeRateApiResponse result = await response.Content.ReadAsAsync<ExchangeRateApiResponse>();
                return result.Rates[toCurrency];
            }

            throw new InvalidOperationException($"Failed to get exchange rates. Status code: {response.StatusCode}");
        }
    }
}

public class ExchangeRateApiResponse
{
    public Dictionary<string, double> Rates { get; set; }
}
