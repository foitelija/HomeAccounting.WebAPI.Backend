using HomeAccounting.Domain.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Application.Interfaces.Infrastructure
{
    public interface IPurchaseReportService
    {
        Task<List<PurchaseReportDto>> GetMonthPurchaseReports(int? month, int? userId = null, int[] categoryIds = null);
    }
}
