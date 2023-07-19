using HomeAccounting.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Queries
{
    public class GetPurchaseDetailRequest : IRequest<Purchase>
    {
        public int Id { get; set; }
    }
}
