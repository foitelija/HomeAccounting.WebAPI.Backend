using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Commands
{
    public class CreatePurchaseCommand : IRequest<BaseServicesResponse>
    {
        public PurchaseCreate Purchase { get; set; }
        public int userId { get; set; }
    }
}
