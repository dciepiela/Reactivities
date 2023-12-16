using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Queries
{
    public class GetActivityByIdHandler:IRequestHandler<GetActivityById, Activity>
    {
        private readonly DataContext _context;

        public GetActivityByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Activity> Handle(GetActivityById request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(activity => activity.Id == request.Id);
            //var activity = await _context.Activities.FindAsync(request.Id);
            return activity;
        }
    }
}
