using MagniseRecruitmentTask.Data;
using MagniseRecruitmentTask.Services;
using Microsoft.EntityFrameworkCore;

namespace MagniseRecruitmentTask.Jobs
{
    public class CoinPriceUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CoinPriceUpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);

                using var scope = _serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var coinApiService = scope.ServiceProvider.GetRequiredService<CoinApiService>();

                var coins = await context.Coins.ToListAsync();

                foreach (var coin in coins)
                {
                    var exchangeRate = await coinApiService.GetRateAsync(coin.Code);

                    coin.Price = exchangeRate.rate;
                    coin.PriceUpdated = exchangeRate.time;

                    context.Update(coin);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
