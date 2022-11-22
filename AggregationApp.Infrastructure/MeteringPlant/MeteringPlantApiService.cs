using AggregationApp.Application.Infrastructure.Contracts;
using AggregationApp.Application.Models;
using System.Text;

namespace AggregationApp.Infrastructure.MeteringPlant
{
    public class MeteringPlantApiService : IMeteringPlantApiService
    {
        public async Task<List<MeteringPlantModel>> LoadCSVData(string year, string month)
        {
            var uri = $"https://data.gov.lt/dataset/1975/download/10766/{year}-{month}.csv";
            var result = new List<MeteringPlantModel>();

            using (var client = new HttpClient())
            {
                using var response = await client.GetAsync(
                        uri, HttpCompletionOption.ResponseHeadersRead);

                using var stream = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(stream, Encoding.UTF8);

                var header = streamReader.ReadLineAsync().Result;

                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    var data = line.Split(',');

                    result.Add(new MeteringPlantModel()
                    {
                        Network = data[0],
                        Title = data[1],
                        Type = data[2],
                        Number = data[3],
                        PPlus = data[4],
                        Date = DateTime.Parse(data[5]),
                        PMinus = data[6],
                    });
                }
            }

            return result;
        }
    }
}
