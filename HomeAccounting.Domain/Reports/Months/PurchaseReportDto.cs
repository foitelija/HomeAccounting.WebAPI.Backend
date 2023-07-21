using HomeAccounting.Domain.Reports.Months;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Statistics
{
    public class PurchaseReportDto
    {
        public string Username { get; set; }
        public List<PurchaseDto> Purchases { get; set; }
    }
}
