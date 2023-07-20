using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Queries
{
    public class GetPurchasesListRequest : IRequest<List<Purchase>>
    {
    }
}
