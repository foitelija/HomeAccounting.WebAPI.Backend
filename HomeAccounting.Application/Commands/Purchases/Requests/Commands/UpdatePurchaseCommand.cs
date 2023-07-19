using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
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
        public int Id { get; set; }
        public Purchase Purchase { get; set; }
    }
}
