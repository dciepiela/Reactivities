using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class EditActivityHandler : IRequestHandler<EditActivity, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditActivityHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(EditActivity request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
            if (activity == null)
            {
                return null;
            }
            
            //jeśli jest null to zostaje poprzednia wartość
            //activity.Title = request.Activity.Title ?? activity.Title;

            //źródło - request.Activity, cel - activity w bazie danych
            _mapper.Map(request.Activity, activity);

            var result = await _context.SaveChangesAsync() > 0;

            if(!result)
            {
                //jeśli nic nie zmienimy i próbujemy edytować
                return Result<Unit>.Failure("Failed to update the activity");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
