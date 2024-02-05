using Application.Activities.Commands;
using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var command = new Details.Query { Username = username };
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

       [HttpPut]
        public async Task<IActionResult> EditProfile(Edit.Command command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username, string predicate)
        {
            var command = new ListActivities.Query { Username = username, Predicate = predicate };
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
    }
}
