using HomeAccounting.Application.Commands.Currencies.Requests.Queries;
using HomeAccounting.Domain.Currency;
using HomeAccounting.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using System.Reflection.Emit;

namespace HomeAccounting.Application.Commands.Currencies.Handlers.Queries
{
    public class GetRatesListRequestHandler : IRequestHandler<GetRatesListRequest, List<Rates>>
    {
        private readonly HttpClient _httpClient;

        public GetRatesListRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Rates>> Handle(GetRatesListRequest request, CancellationToken cancellationToken)
        {
            var rates = new List<Rates>();

            try
            {
                foreach (var curr_ID in Enum.GetValues<RateEnums>())
                {
                    var response = await _httpClient.GetStringAsync($"https://api.nbrb.by/exrates/rates/{(int)curr_ID}");
                    rates.Add(JsonConvert.DeserializeObject<Rates>(response));
                }


                if (request.Cur_ID > 0)
                {
                    var response = await _httpClient.GetStringAsync($"https://api.nbrb.by/exrates/rates/{request.Cur_ID}");
                    rates.Add(JsonConvert.DeserializeObject<Rates>(response));
                }


                return rates;
            }
            catch (HttpRequestException) { throw; }
            catch (Exception) { throw; }
        }
    }
}
