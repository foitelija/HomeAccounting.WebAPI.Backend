using HomeAccounting.Application.Commands.Currencies.Requests.Queries;
using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Domain.Currency;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Convert")]
        public async Task<ActionResult<RatesResponse>> Get([FromQuery] int purchaseCode, [FromQuery] int currCode)
        {
            try
            {
                var ratesResp = new RatesResponse();

                var purchase = await _mediator.Send(new GetPurchaseDetailRequest { Id = purchaseCode });
                var rates = await _mediator.Send(new GetRatesListRequest { Cur_ID = currCode });

                var ratesCompare = new Dictionary<string, decimal>();

                foreach (var rate in rates)
                {
                    decimal convert = purchase.Price / rate.Cur_OfficialRate ?? 0;
                    ratesCompare.Add(rate.Cur_Name, Math.Round(convert,2));
                }

                ratesResp.amountMoneySpent = purchase.Price;
                ratesResp.moneySpentOn = purchase.Category.Name;
                ratesResp.whoMadePurchase = purchase.FamilyMember.Name;
                ratesResp.purchaseComment = purchase.Comment;
                ratesResp.currencyConversion = ratesCompare;

                return ratesResp;
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
