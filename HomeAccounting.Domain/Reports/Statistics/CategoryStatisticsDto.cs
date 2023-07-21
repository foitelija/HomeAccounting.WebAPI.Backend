using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Reports.Statistics
{
    public class CategoryStatisticsDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalPercentage { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
