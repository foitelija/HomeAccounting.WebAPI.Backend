using HomeAccounting.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Purchases
{
    public class PurchaseList : BaseDomainEntity
    {
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Comment { get; set; }
    }
}
