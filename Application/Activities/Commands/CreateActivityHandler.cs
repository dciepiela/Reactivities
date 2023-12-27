using Application.Core;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class CreateActivityHandler : IRequestHandler<CreateActivity, Result<Unit>>
    {
        private readonly DataContext _context;

        public CreateActivityHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(CreateActivity request, CancellationToken cancellationToken)
        {
            _context.Activities.Add(request.Activity);
            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to create activity");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
