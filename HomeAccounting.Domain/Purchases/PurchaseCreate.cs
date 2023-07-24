using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Purchases
{
    public class PurchaseCreate
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Должно быть более 0")]
        public int CategoryId { get; set; }
        [Required]
        [Range(0.01, 999999, ErrorMessage = "Должно быть более 0")]
        public decimal Price { get; set; }
        public string? Comment { get; set; }
    }
}
