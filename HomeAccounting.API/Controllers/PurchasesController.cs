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
        // GET: api/<PurchasesController>
        [HttpGet]
        public async Task<ActionResult<List<Purchase>>> Get()
        {
            var purchases = await _mediator.Send(new GetPurchasesListRequest());
            return Ok(purchases);
        }

        // GET api/<PurchasesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> Get(int id)
        {
            var purchase = await _mediator.Send(new GetPurchaseDetailRequest { Id = id});
            return Ok(purchase);
        }

        // POST api/<PurchasesController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<BaseServicesResponse>> Post([FromBody] PurchaseCreate purchase)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
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
