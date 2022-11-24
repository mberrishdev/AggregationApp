using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService;
using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace AggregationApp.Infrastructure.MeteringPlant
{
    public class ElectricityMeteringService : IElectricityMeteringService
    {
        private readonly ILogger<ElectricityMeteringService> _logger;
        private readonly List<string> _filePaths;
        private const string _filterdObject = "Butas";

        public ElectricityMeteringService(ILogger<ElectricityMeteringService> logger)
        {
            _filePaths = new List<string>()
                        {
                            //"10765/2022-02.csv",
                            "10765/2022-03.csv",
                            "10765/2022-04.csv",
                            "10766/2022-05.csv"
                        };

            _logger = logger;
        }

        public async Task<List<RegionMeteringModel>> GetData(CancellationToken cancellationToken)
        {
            var regionMeteringDetailModels = await LoadData(cancellationToken);
            return GroupData(regionMeteringDetailModels);
        }

        private async Task<List<RegionMeteringDetailModel>> LoadData(CancellationToken cancellationToken)
        {
            var regionDetailModelList = new List<List<RegionMeteringDetailModel>>();
            foreach (var filePath in _filePaths)
            {
                try
                {
                    using var client = new HttpClient();
                    var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                    };

                    var stream = await client
                        .GetStreamAsync($"https://data.gov.lt/dataset/1975/download/{filePath}", cancellationToken);

                    using var reader = new StreamReader(stream);
                    using var csv = new CsvReader(reader, csvConfiguration);

                    var data = csv.GetRecords<RegionMeteringDetailModel>()
                        .Where(y => y.OBT_PAVADINIMAS.Equals(_filterdObject)).Take(10)
                        .ToList();

                    regionDetailModelList.Add(data);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error,
                        ex.Message);
                    throw;
                }
            }

            return regionDetailModelList
                .Aggregate((i, j) => i.Concat(j).ToList());
        }

        private List<RegionMeteringModel> GroupData(List<RegionMeteringDetailModel> regionMeteringDetailModels)
        {
            if (regionMeteringDetailModels.Any())
                return regionMeteringDetailModels.GroupBy(x => x.TINKLAS)
                        .Select(y => new RegionMeteringModel
                        {
                            TINKLAS = y.Key,
                            RegionMeteringDetailModel = y.ToList()
                        }).ToList();

            return new List<RegionMeteringModel>();
        }
    }
}
