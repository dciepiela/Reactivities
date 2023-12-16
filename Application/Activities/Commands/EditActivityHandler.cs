using AutoMapper;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class EditActivityHandler : IRequestHandler<EditActivity>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditActivityHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(EditActivity request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);
            
            //jeśli jest null to zostaje poprzednia wartość
            //activity.Title = request.Activity.Title ?? activity.Title;

            //źródło - request.Activity, cel - activity w bazie danych
            _mapper.Map(request.Activity, activity);

            await _context.SaveChangesAsync();
        }
    }
}
