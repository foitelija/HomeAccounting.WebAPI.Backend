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

        /// <summary>
        ///  Метод получения всех затрат за выбранный месяц
        /// </summary>
        /// <remarks>
        /// Выбранный месяц может быть пустым, тогда вернёт за весь период времени. 
        /// Настроена пагинация. 
        /// Пример возвращается как список: 
        /// 
        ///     GET/api/reports/expenses/month
        ///     {
        ///      "items": [
        ///        {
        ///          "username": "Nikolay",
        ///          "purchases": [
        ///            {
        ///              "id": 2,
        ///              "familyMember": {
        ///                "familyMemberId": 1,
        ///                "name": "Nikolay"
        ///              },
        ///              "category": {
        ///                "categoryId": 5,
        ///                "name": "Развлечения"
        ///              },
        ///              "price": 32,
        ///              "comment": "claim test 2",
        ///              "created": "2023-07-19T16:00:52.5934997"
        ///            }
        ///          ]
        ///        }
        ///      ],
        ///      "totalCount": 13,
        ///      "pageNumber": 1,
        ///      "pageSize"
        ///       }
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Нет данных.</response>
        /// <response code="500">Иная ошибка.</response>
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

        /// <summary>
        ///  Метод получения статистики за выбранный период
        /// </summary>
        /// <remarks>
        /// В методе статистики за период показан процент и сумма трат по каждой категории и общую сумму для каждого члена семьи.
        /// Можно не указывать период за который требуется вывести затраты. 
        /// Настроена пагинация. 
        /// Пример возвращается как список: 
        /// 
        ///     GET/api/reports/expenses/month
        ///         {
        ///           "items": {
        ///             "statisticsByCategory": [
        ///               {
        ///                 "categoryId": 5,
        ///                 "categoryName": "Развлечения",
        ///                 "totalPercentage": 100,
        ///                 "totalAmount": 32
        ///               }
        ///             ],
        ///             "statisticsByFamilyMember": [
        ///               {
        ///                 "familyMemberId": 1,
        ///                 "familyMemberName": "Nikolay",
        ///                 "totalAmount": 32
        ///               }
        ///             ],
        ///             "totalAmount": 1478.95
        ///           },
        ///           "totalCount": 13,
        ///           "pageNumber": 1,
        ///           "pageSize": 1
        ///         }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Нет данных или конечнкая дата меньше начальной.</response>
        /// <response code="500">Иная ошибка.</response>
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
