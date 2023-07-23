using HomeAccounting.Application.Commands.Categories.Requests.Commands;
using HomeAccounting.Application.Commands.Categories.Requests.Queries;
using HomeAccounting.Application.Responses;
using HomeAccounting.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAccounting.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Список всех категорий
        /// </summary>
        /// <remarks>
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// 
        /// Пример возвращается как список List: 
        /// 
        ///     GET/
        ///     [
        ///      {
        ///        "id": 1,
        ///        "dateCreated": "2023-07-23T13:50:48.074Z",
        ///        "name": "string1"
        ///        "ColorHexCode" "#ff33333",
        ///             },
        ///             {
        ///        "id": 2,
        ///        "dateCreated": "2023-07-23T13:50:48.074Z",
        ///        "name": "string2"
        ///        "ColorHexCode" "#ff33333"
        ///                 }
        ///             ]
        ///         }
        ///     ]
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение или иная другая ошибка, которая прописывается в поле errorMessage.</response>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await _mediator.Send(new GetCategoriesListRequest());
            return Ok(categories);
        }

        /// <summary>
        /// Уникальная категория по ID
        /// </summary>
        /// <remarks>
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// 
        /// Пример возвращает категорию: 
        /// 
        ///     GET/id   
        ///         {
        ///          "id": 1,
        ///          "dateCreated": "2023-07-23T13:50:48.074Z",
        ///          "name": "string1",
        ///          "ColorHexCode" "#ff33333"
        ///         }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        // GET: api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _mediator.Send(new GetCategoryDetailRequest { Id = id });
            return Ok(category);
        }


        /// <summary>
        /// Добавление новой категории
        /// </summary>
        /// <remarks>
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// 
        /// Пример запроса: 
        /// 
        ///     Post/
        ///         {
        ///          "id": 0, - указывать не нужно, EF Core ставит сам.
        ///          "dateCreated": "2023-07-23T13:50:48.074Z",
        ///          "name": "Новая категория затрат",
        ///          "ColorHexCode" "#ff33333"
        ///         }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение или иная другая ошибка, которая прописывается в поле errorMessage.</response>
        /// <response code="400">Что-то пошло не так</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseServicesResponse>> Post([FromBody] Category category)
        {
            var command = new CreateCategoryCommand { Category = category };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Добавление новой категории
        /// </summary>
        /// <remarks>
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка. В BaseServiceResponse есть ErrorMessage, где можно посмотреть, что за ошибка.
        /// Передаём параметром ID обновляемой категории.
        /// Пример запроса: 
        /// 
        ///     PUT/id
        ///         {
        ///             "id": 0, - указывать не нужно
        ///          "dateCreated": "2023-07-23T13:56:09.748Z",
        ///          "name": "Новая категория затрат 2",
        ///          "ColorHexCode" "#ff33333"
        ///         }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение или иная другая ошибка, которая прописывается в поле errorMessage.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseServicesResponse>> Put(int id, [FromBody] Category category)
        {
            var command = new UpdateCategoryCommand { Id = id, Category = category };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <remarks>
        /// Возвращает формат BaseServiceResponse, 200 код, но может быть ошибка.
        /// Указывается ID удаляемой категории. Если она уже используется в какой-нибудь покупке, из метода возвращается BaseServiceResponse  с кодом 200 и сообщением, 
        /// "You cannot delete a category because it is being used"
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение или иная другая ошибка, которая прописывается в поле errorMessage.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseServicesResponse>> Delete(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
