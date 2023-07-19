using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Queries
{
    public class GetPurchaseDetailRequestHandler : IRequestHandler<GetPurchaseDetailRequest, Purchase>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public GetPurchaseDetailRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Purchase> Handle(GetPurchaseDetailRequest request, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseRepository.GetPurchasesWithDetailsAsync(request.Id);
            if (purchase == null)
            {
                return new Purchase();
            }
            return purchase;
        }
    }
}
