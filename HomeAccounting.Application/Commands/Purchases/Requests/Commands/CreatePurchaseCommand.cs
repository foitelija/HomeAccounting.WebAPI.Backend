using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using MediatR;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Commands
{
    public class CreatePurchaseCommand : IRequest<BaseServicesResponse>
    {
        public Purchase Purchase { get; set; }
    }
}
