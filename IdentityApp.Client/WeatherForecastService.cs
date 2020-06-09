using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityApp.Data;

namespace IdentityApp.Client
{
    public class WeatherForecastService
    {
        private readonly HttpClient _client;

        public WeatherForecastService(WeatherServiceSettings settings)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _client = new HttpClient(clientHandler);
            _client.BaseAddress = settings.Url;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var data = await _client.GetAsync("weatherforecast");
            if (data.StatusCode != HttpStatusCode.OK)
                return new WeatherForecast[0];
            var body = await data.Content.ReadAsStringAsync();
            var output = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WeatherForecast>>(body);
            return output.ToArray();
        }
    }
}