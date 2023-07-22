using HomeAccounting.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Commands
{
    public class DeletePurchaseCommand : IRequest<BaseServicesResponse>
    {
        public int Id { get; set; }
        public int userID { get; set; }
    }
}
