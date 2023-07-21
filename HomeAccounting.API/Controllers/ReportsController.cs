using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Filters;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Reports.Statistics;
using HomeAccounting.Domain.Statistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPurchaseReportService _purchaseReportService;

        public ReportsController(IMediator mediator, IPurchaseReportService purchaseReportService)
        {
            _mediator = mediator;
            _purchaseReportService = purchaseReportService;
        }

        [HttpGet]
        [Route("expenses/month")]
        public async Task<ActionResult<PageResponse<List<PurchaseReportDto>>>> GetExpensesByMonth([FromQuery] int? month, [FromQuery] int? userId = null,
            [FromQuery] int[]? categoryIds = null, int page = 1, int pageSize = 5)
        {
            try
            {
                var purchaseMonthReports = await _purchaseReportService.GetMonthPurchaseReports(month,userId,categoryIds, page, pageSize);
                
                if(purchaseMonthReports.Items == null || purchaseMonthReports.Items.Count < 1)
                {
                    return BadRequest("Data not found.");
                }

                return Ok(purchaseMonthReports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("expenses/statistics")]
        public async Task<ActionResult<PageResponse<StatisticsResultDto>>> GetExpenseStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? userId = null, [FromQuery] int[] categoryIds = null, int page = 1, int pageSize = 5)
        {
            if (endDate < startDate)
            {
                return BadRequest($"{endDate} should be greater than or equal to {startDate}.");
            }

            var purchasePeriodReport = await  _purchaseReportService.GetStatisticsPurchaseReports(startDate, endDate, userId, categoryIds, page, pageSize);

            if(purchasePeriodReport.Items.StatisticsByFamilyMember.Count < 1 || purchasePeriodReport.Items.StatisticsByCategory.Count < 1)
            {
                return BadRequest("Data not found.");
            }

            return Ok(purchasePeriodReport);
        }
    }
}
