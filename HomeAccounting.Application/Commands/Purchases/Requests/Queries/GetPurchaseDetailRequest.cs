using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Commands.Purchases.Requests.Queries
{
    public class GetPurchaseDetailRequest : IRequest<PurchaseList>
    {
        public int Id { get; set; }
    }
}
