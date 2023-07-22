using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Purchases
{
    public class PurchaseUpdate
    {
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Comment { get; set; }
    }
}
