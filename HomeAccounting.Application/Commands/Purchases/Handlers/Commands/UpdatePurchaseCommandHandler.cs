﻿using HomeAccounting.Application.Commands.Purchases.Requests.Commands;
using HomeAccounting.Application.Interfaces.Infrastructure;
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

                purchase.FamilyMemberId = request.Purchase.FamilyMemberId;
                purchase.CategoryId = request.Purchase.CategoryId;

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