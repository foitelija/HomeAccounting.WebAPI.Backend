using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Queries
{
    public class GetPurchaseDetailRequestHandler : IRequestHandler<GetPurchaseDetailRequest, PurchaseList>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public GetPurchaseDetailRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<PurchaseList> Handle(GetPurchaseDetailRequest request, CancellationToken cancellationToken)
        {
            var purchases = await GetPurchaseWithoutSecretData(request.Id);
            return purchases;
        }

        private async Task<PurchaseList> GetPurchaseWithoutSecretData(int id)
        {
            var secretPurchases = await _purchaseRepository.GetPurchasesWithDetailsAsync(id);

            if(secretPurchases == null)
            {
                return new PurchaseList();
            }

            var purchases = new PurchaseList
            {
                Id = secretPurchases.Id,
                UserName = secretPurchases.FamilyMember.Name,
                UserLogin = secretPurchases.FamilyMember.Login,
                Category = secretPurchases.Category,
                CategoryId = secretPurchases.CategoryId,
                Price = secretPurchases.Price,
                Comment = secretPurchases.Comment,
                DateCreated = secretPurchases.DateCreated
            };

            return purchases;
        }
    }
}
