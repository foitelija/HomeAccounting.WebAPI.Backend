using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Queries
{
    public class GetPurchasesListRequestHandler : IRequestHandler<GetPurchasesListRequest, List<PurchaseList>>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public GetPurchasesListRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<List<PurchaseList>> Handle(GetPurchasesListRequest request, CancellationToken cancellationToken)
        {
            var purchases = await GetPurchasesListWithoutSecretData();
            return purchases;
        }

        private async Task<List<PurchaseList>> GetPurchasesListWithoutSecretData()
        {
            var secretPurchases = await _purchaseRepository.GetPurchasesListWithDetailsAsync();

            var purchases = secretPurchases.Select(secretPurchases => new PurchaseList
            {
                Id = secretPurchases.Id,
                UserName = secretPurchases.FamilyMember.Name,
                UserLogin = secretPurchases.FamilyMember.Login,
                Category = secretPurchases.Category,
                CategoryId = secretPurchases.CategoryId,
                Price = secretPurchases.Price,
                Comment = secretPurchases.Comment,
                DateCreated = secretPurchases.DateCreated
            }).ToList();

            return purchases;
        }

    }
}
