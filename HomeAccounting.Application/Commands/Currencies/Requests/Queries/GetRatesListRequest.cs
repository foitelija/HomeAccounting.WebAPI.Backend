using HomeAccounting.Domain.Currency;
using MediatR;

namespace HomeAccounting.Application.Commands.Currencies.Requests.Queries
{
    public class GetRatesListRequest : IRequest<List<Rates>>
    {
        public int Cur_ID { get; set; }
    }
}
