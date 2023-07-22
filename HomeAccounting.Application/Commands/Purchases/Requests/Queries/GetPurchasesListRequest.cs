using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Queries
{
    public class GetPurchasesListRequest : IRequest<List<PurchaseList>>
    {
    }
}
