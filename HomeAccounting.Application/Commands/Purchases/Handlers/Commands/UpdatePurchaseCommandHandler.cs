using HomeAccounting.Application.Commands.Purchases.Requests.Commands;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Commands
{
    public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, BaseServicesResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<BaseServicesResponse> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();
            try
            {
                var purchase = await _purchaseRepository.GetPurchasesWithDetailsAsync(request.Id);

                if (purchase.FamilyMemberId != request.userId)
                {
                    throw new Exception("Вам ограничен доступ к этим данным.");
                }

                purchase.CategoryId = request.Purchase.CategoryId;
                purchase.Comment = request.Purchase.Comment;
                purchase.Price = request.Purchase.Price;

                await _purchaseRepository.Update(purchase);

                response.Id = purchase.Id;
                response.Message = "Updated.";
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.Message = "Something went wrong on update.";
            }

            
            return response;
        }
    }
}
