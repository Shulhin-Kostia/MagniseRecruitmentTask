using MagniseRecruitmentTask.Models;
using MagniseRecruitmentTask.Services;

namespace MagniseRecruitmentTask.Data
{
    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                var coinApiService = serviceScope.ServiceProvider.GetRequiredService<CoinApiService>();
                string[] coinCodes = ["BTC"/*, "DOGE", "ETH", "USDT", "TON"*/];

                context.Database.EnsureCreated();

                foreach (var coinCode in coinCodes)
                {
                    var exchangeRate = coinApiService.GetRateAsync(coinCode).GetAwaiter().GetResult();

                    var coin = new Coin()
                    {
                        Code = coinCode,
                        Price = exchangeRate.rate,
                        PriceUpdated = exchangeRate.time
                    };

                    context.Add(coin);
                }

                context.SaveChanges();
            }
        }
    }
}
