using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Reports.Statistics
{
    public class FamilyMemberStatisticsDto
    {
        public int FamilyMemberId { get; set; }
        public string FamilyMemberName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
