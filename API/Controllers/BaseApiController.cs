using Application.Core;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        // jeśli jest to null to przypisz mu MediatR
        protected IMediator Mediator => _mediator ??= 
            HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null)
            {
                return NotFound();
            }
            if (result.isSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            if (result.isSuccess && result.Value == null)
            {
                return NotFound();
            }
            return BadRequest(result.Error);
        }
    }
}
