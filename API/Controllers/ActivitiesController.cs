using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController:BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<IActionResult> GetActivities()
        {
            var query = new GetAllActivities();
            var response = await Mediator.Send(query);
            return HandleResult(response);
        }

        [HttpGet("{id}")] //api/activities/fdfdfdfdf
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            var query = new GetActivityById { Id = id };
            var response = await Mediator.Send(query);

            return HandleResult(response);
        }

        // IActionResult - Ok(), NotFound(), BadRequest()
        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            var command = new CreateActivity { Activity =  activity };
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            var result = await Mediator.Send(new EditActivity { Activity = activity });
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var command = new DeleteActivity { Id = id };
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

    }
}
