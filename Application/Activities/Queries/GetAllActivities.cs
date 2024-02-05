using Application.Core;
using MediatR;

namespace Application.Activities.Queries
{
    public class GetAllActivities : IRequest<Result<PagedList<ActivityDto>>>
    {
        public ActivityParams Params { get; set; }
    }
}
