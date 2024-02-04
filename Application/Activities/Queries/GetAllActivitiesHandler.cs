using Application.Core;
using Application.Interfaces;
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
        private readonly IUserAccessor _userAccessor;

        public GetAllActivitiesHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<ActivityDto>>> Handle(GetAllActivities request, CancellationToken cancellationToken)
        {
            // var activities = await _context.Activities
            //     .Include(a => a.Attendees)
            //     .ThenInclude(u => u.AppUser)
            //     .ToListAsync();

            var activities = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider, 
                    new {currentUsername = _userAccessor.GetUsername()})
                .ToListAsync();

            // var activitiesToReturn = _mapper.Map<List<ActivityDto>>(activities);
            return Result<List<ActivityDto>>.Success(activities);
        }
    }
}
