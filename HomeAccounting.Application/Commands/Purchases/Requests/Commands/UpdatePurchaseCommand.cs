using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Commands
{
    public class UpdatePurchaseCommand : IRequest<BaseServicesResponse>
    {
        public int userId { get; set; }
        public int Id { get; set; }
        public PurchaseUpdate Purchase { get; set; }
    }
}
