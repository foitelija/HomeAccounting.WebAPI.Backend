using HomeAccounting.Application.Commands.Purchases.Requests.Commands;
using HomeAccounting.Application.Commands.Purchases.Requests.Queries;
using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using HomeAccounting.Domain.Purchases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAccounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PurchasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchasesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить все покупки
        /// </summary>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet]
        public async Task<ActionResult<List<Purchase>>> Get()
        {
            var purchases = await _mediator.Send(new GetPurchasesListRequest());
            return Ok(purchases);
        }

        /// <summary>
        /// Получить покупку по ID
        /// </summary>
        /// <remarks>
        /// Параметром преедаётся ID продукта 
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> Get(int id)
        {
            var purchase = await _mediator.Send(new GetPurchaseDetailRequest { Id = id});
            return Ok(purchase);
        }

        /// <summary>
        /// Добавление новой покупки
        /// </summary>
        /// <remarks>
        /// Доступно только авторизованным пользователям
        /// Пример запроса:
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// ID пользователя тянется автоматически из Клэймов
        /// 
        ///     POST/
        ///     {
        ///        "categoryId" : 1, 
        ///        "price" : 111,
        ///        "comment": "Наушники Razer"
        ///     }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка пользователя, почему-то не нашёл его.</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseServicesResponse>> Post([FromBody] PurchaseCreate purchase)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrEmpty(userId))
            {
                return BadRequest("Ошибка пользователя");
            }

            var command = new CreatePurchaseCommand { Purchase = purchase, userId = int.Parse(userId) };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<PurchasesController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseServicesResponse>> Put(int id, [FromBody] PurchaseUpdate purchase)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
            var command = new UpdatePurchaseCommand { Id = id, Purchase = purchase, userId = int.Parse(userId) };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        // DELETE api/<PurchasesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<BaseServicesResponse>> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
            var command = new DeletePurchaseCommand { Id = id, userID = int.Parse(userId) };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
