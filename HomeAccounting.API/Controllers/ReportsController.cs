using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Filters;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Domain;
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
        public async Task<ActionResult<List<PurchaseReportDto>>> GetExpensesByMonth([FromQuery] int? month, [FromQuery] int? userId = null,
            [FromQuery] int[] categoryIds = null, int page = 1, int pageSize = 2)
        {
            try
            {
                var purchaseMonthReports = await _purchaseReportService.GetMonthPurchaseReports(month,userId,categoryIds);
                var pageFilter = new PaginationFilter(page, pageSize);
                var pagedPurchaseReports = purchaseMonthReports.Skip((pageFilter.PageNumber - 1) * pageFilter.PageSize).Take(pageFilter.PageSize).ToList();

                return Ok(pagedPurchaseReports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("expenses/statistics")]
        public async Task<ActionResult> GetExpenseStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? userId = null, [FromQuery] int[] categoryIds = null, int page = 1, int pageSize = 2)
        {

            if (endDate < startDate)
            {
                return BadRequest($"{endDate} should be greater than or equal to {startDate}.");
            }

            await Task.Delay(100);
            return Ok();
        }
    }
}
