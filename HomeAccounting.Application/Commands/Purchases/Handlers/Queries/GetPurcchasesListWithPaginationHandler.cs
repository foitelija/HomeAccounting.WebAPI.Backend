using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Queries
{
    public class GetPurcchasesListWithPaginationHandler : IRequestHandler<GetPurchasesListWithPaginationRequest, List<Purchase>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public GetPurcchasesListWithPaginationHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<List<Purchase>> Handle(GetPurchasesListWithPaginationRequest request, CancellationToken cancellationToken)
        {
            var purchasesWithPagination = await _purchaseRepository.GetPurchasesListWithDetailsAndPaginationAsync(request.PageNumber, request.PageSize);
            return purchasesWithPagination;
        }
    }
}
