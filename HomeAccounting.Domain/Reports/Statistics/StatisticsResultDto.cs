using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Domain.Reports.Statistics
{
    public class StatisticsResultDto
    {
        public List<CategoryStatisticsDto> StatisticsByCategory { get; set; }
        public List<FamilyMemberStatisticsDto> StatisticsByFamilyMember { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
