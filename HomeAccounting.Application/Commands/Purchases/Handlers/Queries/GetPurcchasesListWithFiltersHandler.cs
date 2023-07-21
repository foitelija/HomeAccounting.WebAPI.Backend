using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Queries
{
    public class GetPurcchasesListWithFiltersHandler : IRequestHandler<GetPurchasesListWithFiltersRequest, List<Purchase>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public GetPurcchasesListWithFiltersHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<List<Purchase>> Handle(GetPurchasesListWithFiltersRequest request, CancellationToken cancellationToken)
        {
            var purchasesWithPagination = await _purchaseRepository.GetPurchasesListWithFiltersAsync(request.month, request.userId, request.categoryIds);
            return purchasesWithPagination;
        }
    }
}
