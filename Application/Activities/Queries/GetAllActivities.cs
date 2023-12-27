using Application.Core;
using Domain;
using MediatR;
using System.Collections.Generic;

namespace Application.Activities.Queries
{
    public class GetAllActivities : IRequest<Result<List<Activity>>>
    {
    }
}
