using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class DeleteActivityHandler : IRequestHandler<DeleteActivity>
    {
        private readonly DataContext _context;

        public DeleteActivityHandler(DataContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteActivity request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Id);

            _context.Remove(activity);

            await _context.SaveChangesAsync();
        }
    }
}
