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
        /// <remarks>
        /// Пример возвращаемых значений
        /// 
        ///     Get/
        ///     {
        ///        "userName": "Nikolay",
        ///        "userLogin": "string",
        ///        "category": {
        ///          "name": "Развлечения",
        ///          "id": 5,
        ///          "dateCreated": null
        ///        },
        ///        "categoryId": 5,
        ///        "price": 32,
        ///        "comment": "claim test 2",
        ///        "id": 2,
        ///        "dateCreated": "2023-07-19T16:00:52.5934997"
        ///     },
        ///     {
        ///         "next value"
        ///     }
        ///    
        /// </remarks>    
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet]
        public async Task<ActionResult<List<PurchaseList>>> Get()
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
        /// <response code="401">Ограничение доступа.</response>
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

        /// <summary>
        /// Обновление уже существующей покупки
        /// </summary>
        /// <remarks>
        /// Доступно только авторизованным пользователям
        /// Пример запроса:
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// ID пользователя тянется автоматически из Клэймов
        /// Указывается ID изменяемой покупки, потом как JSON передаём 
        /// 
        ///     Было:
        ///     PUT/
        ///     {
        ///        "categoryId" : 1, 
        ///        "price" : 111, 
        ///        "comment": "Наушники Razer"
        ///     }
        ///     Стало
        ///     PUT/
        ///     {
        ///        "categoryId" : 1, 
        ///        "price" : 130, 
        ///        "comment": "Наушники Logitech"
        ///     }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="401">Ограничение доступа.</response>
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
        /// <summary>
        /// Удаление покупки
        /// </summary>
        /// <remarks>
        /// Доступно только авторизованным пользователям
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// ID пользователя тянется автоматически из Клэймов
        /// Указывается ID удаляемой покупки
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="401">Ограничение доступа.</response>
        /// <response code="400">Какая-нибудь другая ошибка, в BaseServiceResponse есть ErrorMessage.</response>
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
