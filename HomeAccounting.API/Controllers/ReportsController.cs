using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Filters;
using HomeAccounting.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("expenses/month")]
        public async Task<ActionResult<List<Purchase>>> GetExpensesByMonth([FromQuery] int? month, [FromQuery] int? userId = null,
            [FromQuery] int[] categoryIds = null, int page = 1, int pageSize = 2)
        {
            try
            {
                var pageFilter = new PaginationFilter(page, pageSize);
                var expenses = await _mediator.Send(new GetPurchasesListWithFiltersRequest { month = month, userId = userId, categoryIds = categoryIds });

                var pagedData = expenses.Skip((pageFilter.PageNumber - 1) * pageFilter.PageSize).Take(pageFilter.PageSize).ToList();

                return Ok(pagedData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("expenses/statistics")]
        public async Task<ActionResult> GetExpenseStatistics()
        {
            await Task.Delay(100);
            return Ok();
        }
    }
}
