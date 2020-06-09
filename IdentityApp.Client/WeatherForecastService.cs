using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IdentityApp.Api;

namespace Identity.Web.Data
{
    public class WeatherForecastService
    {
        private readonly HttpClient _client;

        public WeatherForecastService(WeatherServiceSettings settings)
        {
            _client = new HttpClient();
            _client.BaseAddress = settings.Url;
        }
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

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