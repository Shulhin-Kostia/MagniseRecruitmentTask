using CoinAPI.REST.V1;

namespace MagniseRecruitmentTask.Services
{
    public class CoinApiService
    {
        private CoinApiRestClient _coinApiClient;

        public CoinApiService(IConfiguration config)
        {
            _coinApiClient = new CoinApiRestClient(config.GetSection("CoinapiApiKey").Value!);
        }

        public async Task<Exchangerate> GetRateAsync(string assetId)
        {
            try
            {
                return await _coinApiClient.Exchange_rates_get_specific_rateAsync(assetId, "USD");
            }
            catch
            {
                return default;
            }
        }
    }
}
