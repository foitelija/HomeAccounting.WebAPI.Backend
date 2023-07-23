﻿using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Domain.Currency;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(IMediator mediator, ICurrencyService currencyService)
        {
            _mediator = mediator;
            _currencyService = currencyService;
        }

        [HttpGet("Convert")]
        public async Task<ActionResult<СurrencyСonversion>> Get([FromQuery] int purchaseCode, [FromQuery] int currCode)
        {
            try
            {
                if(purchaseCode == 0 || purchaseCode < 0 )
                {
                    throw new ArgumentException("Введите код покупки для конвертации");
                }

                var purchase = await _mediator.Send(new GetPurchaseDetailRequest { Id = purchaseCode });   
                var conversion = await _currencyService.GetCurrencyResponseAsync(currCode, purchase);

                return conversion;
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Введите код покупки для конвертации"))
                {
                    return BadRequest(ex.Message);
                }

                return StatusCode(500, ex.Message);
            }
        }
    }
}
