using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController:BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
        {
            var query = new GetAllActivities();
            var response = await Mediator.Send(query,ct);
            return response;
        }

        [HttpGet("{id}")] //api/activities/fdfdfdfdf
        public async Task<ActionResult<Activity>> GetActivityById(Guid id)
        {
            var query = new GetActivityById { Id = id };
            var response = await Mediator.Send(query);
            if (response == null)
            {
                return NotFound($"No user with profile ID {id}");
            }

            return response;
        }

        // IActionResult - Ok(), NotFound(), BadReques()
        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            var command = new CreateActivity { Activity =  activity };
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            await Mediator.Send(new EditActivity { Activity = activity });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var command = new DeleteActivity { Id = id };
            await Mediator.Send(command);
            return Ok();
        }

    }
}
