using Domain;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class CreateActivityHandler : IRequestHandler<CreateActivity>
    {
        private readonly DataContext _context;

        public CreateActivityHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateActivity request, CancellationToken cancellationToken)
        {
            _context.Activities.Add(request.Activity);
            await _context.SaveChangesAsync();
        }
    }
}
