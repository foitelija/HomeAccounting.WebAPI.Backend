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

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await _mediator.Send(new GetCategoriesListRequest());
            return Ok(categories);
        }

        // GET: api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _mediator.Send(new GetCategoryDetailRequest { Id = id });
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseServicesResponse>> Post([FromBody] Category category)
        {
            var command = new CreateCategoryCommand { Category = category };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseServicesResponse>> Put(int id, [FromBody] Category category)
        {
            var command = new UpdateCategoryCommand { Id = id, Category = category };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

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
