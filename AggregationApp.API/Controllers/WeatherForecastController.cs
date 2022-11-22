using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AggregationApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var uri = "https://data.gov.lt/dataset/1975/download/10766/2022-05.csv";
            var httpClient = new HttpClient();
            using var response = httpClient.GetAsync(
                    uri, HttpCompletionOption.ResponseHeadersRead);
            using var stream = response.Result.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream.Result, Encoding.UTF8);
            var a = streamReader.ReadLineAsync().Result;
            var res = new List<MeteringPlant>();
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLineAsync();
                var v = line.Result;
                var x = v.Split(',');
                res.Add(new MeteringPlant()
                {
                    Network = x[0],
                    Title = x[1],
                    Type = x[2],
                    Number = x[3],
                    PPlus = x[4],
                    Date = DateTime.Parse(x[5]),
                    PMinus = x[6],
                });
                //_logger.LogError(line.Result.ToString());
                //var address = GetAddress(line);
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
    public class MeteringPlant
    {
        public string Network { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string PPlus { get; set; }
        public string PMinus { get; set; }
        public DateTime Date { get; set; }
    }
}