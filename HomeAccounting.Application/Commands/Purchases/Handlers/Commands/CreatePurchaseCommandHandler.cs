using HomeAccounting.Application.Commands.Purchases.Requests.Commands;
using HomeAccounting.Application.Interfaces.Persistence;
using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Handlers.Commands
{
    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, BaseServicesResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, ICategoryRepository categoryRepository)
        {
            _purchaseRepository = purchaseRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseServicesResponse> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseServicesResponse();

            try
            {
                var category = await _categoryRepository.GetById(request.Purchase.CategoryId);
                
                var purchase = (new Purchase
                {
                    CategoryId = category.Id,
                    Category = category,
                    FamilyMemberId = request.Purchase.FamilyMemberId,
                    Price = request.Purchase.Price,
                    Comment = request.Purchase.Comment,
                });



                purchase = await _purchaseRepository.Add(purchase);

                response.Message = "Creation.";
                response.Id = request.Purchase.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.Message = "Something wrong";
            }

            return response;
        }
    }
}
