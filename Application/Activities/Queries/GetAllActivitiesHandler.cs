using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Queries
{
    public class GetAllActivitiesHandler : IRequestHandler<GetAllActivities, List<Activity>>
    {
        private readonly DataContext _context;

        public GetAllActivitiesHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> Handle(GetAllActivities request, CancellationToken cancellationToken)
        {
            var activities = await _context.Activities.ToListAsync();
            return activities;
        }
    }
}
