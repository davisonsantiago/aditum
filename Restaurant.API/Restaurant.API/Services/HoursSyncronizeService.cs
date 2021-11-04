using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant.API.Models;
using Restaurant.API.Models.DTO;
using Restaurant.API.Models.Filter;
using Restaurant.API.Repository.Interface;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.API.Services
{
    public class HoursSyncronizeService : IHostedService, IDisposable
    {
        private readonly IRestaurantRepository _repository;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private Timer _timer;

        public HoursSyncronizeService(
            IRestaurantRepository repository,
            IConfiguration config)
        {
            _repository = repository;
            _config = config;
            _logger = new LoggerFactory().CreateLogger<HoursSyncronizeService>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }
        }

        public void ReadCSVFile(object state)
        {
            using (var reader = new StreamReader(_config.GetValue<string>("csvPath"), Encoding.UTF8))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<RestaurantDTO>().ToList();

                    foreach (var item in records)
                    {
                        var hours = item.OpenHours.Split('-');

                        var restaurant = new RestaurantModel
                        {
                            Name = item.RestaurantName,
                            Open = TimeSpan.Parse(hours[0]),
                            Close = TimeSpan.Parse(hours[1])
                        };

                        var result = _repository.Read(new RestaurantFilter { Name = restaurant.Name }).FirstOrDefault();

                        if (result == null)
                        {
                            _repository.Create(restaurant);
                        }
                        else
                        {
                            restaurant.Id = result.Id;
                            _repository.Update(restaurant);
                        }
                    }
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service start");

            _timer = new Timer(ReadCSVFile, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service stop");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
