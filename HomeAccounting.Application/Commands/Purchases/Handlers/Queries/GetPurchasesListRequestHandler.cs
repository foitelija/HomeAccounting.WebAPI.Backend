using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Queries
{
    public class GetPurchasesListRequestHandler : IRequestHandler<GetPurchasesListRequest, List<Purchase>>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public GetPurchasesListRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<List<Purchase>> Handle(GetPurchasesListRequest request, CancellationToken cancellationToken)
        {
            var purchases = await _purchaseRepository.GetPurchasesListWithDetailsAsync();
            return purchases;
        }
    }
}
