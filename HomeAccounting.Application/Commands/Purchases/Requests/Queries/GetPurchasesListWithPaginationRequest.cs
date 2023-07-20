using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Queries
{
    public class GetPurchasesListWithPaginationRequest : IRequest<List<Purchase>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
