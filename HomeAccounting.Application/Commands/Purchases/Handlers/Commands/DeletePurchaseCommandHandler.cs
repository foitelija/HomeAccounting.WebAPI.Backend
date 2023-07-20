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
    public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, BaseServicesResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<BaseServicesResponse> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();
            try
            {
                var purchase = await _purchaseRepository.GetById(request.Id);

                await _purchaseRepository.Delete(purchase);

                response.Id = purchase.Id;
                response.Message = "Deleted";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.Message = "Something went wrong when delete.";
            }

            return response;
        }
    }
}
