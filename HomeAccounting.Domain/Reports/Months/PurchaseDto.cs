using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Reports.Months
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public FamilyMemberDto? FamilyMember { get; set; }
        public CategoryDto? Category { get; set; }
        public decimal Price { get; set; }
        public string? Comment { get; set; }
        public DateTime? Created { get; set;}
    }
}
