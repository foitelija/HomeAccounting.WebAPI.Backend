using HomeAccounting.Application.Commands.Categories.Requests.Queries;
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
    }
}
