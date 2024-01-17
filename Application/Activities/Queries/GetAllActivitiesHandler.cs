using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetAllActivitiesHandler : IRequestHandler<GetAllActivities,Result<List<ActivityDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetAllActivitiesHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<ActivityDto>>> Handle(GetAllActivities request, CancellationToken cancellationToken)
        {
            // var activities = await _context.Activities
            //     .Include(a => a.Attendees)
            //     .ThenInclude(u => u.AppUser)
            //     .ToListAsync();

            var activities = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // var activitiesToReturn = _mapper.Map<List<ActivityDto>>(activities);
            return Result<List<ActivityDto>>.Success(activities);
        }
    }
}
