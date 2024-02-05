using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Queries
{
    public class GetAllActivitiesHandler : IRequestHandler<GetAllActivities,Result<PagedList<ActivityDto>>>
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

        public async Task<Result<PagedList<ActivityDto>>> Handle(GetAllActivities request, CancellationToken cancellationToken)
        {
            // var activities = await _context.Activities
            //     .Include(a => a.Attendees)
            //     .ThenInclude(u => u.AppUser)
            //     .ToListAsync();

            var query = _context.Activities
                .Where(a => a.Date >= request.Params.StartDate)
                .OrderBy(a => a.Date)
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider,
                    new { currentUsername = _userAccessor.GetUsername() })
                .AsQueryable();

            if(request.Params.IsGoing && !request.Params.IsHost)
            {
                query = query.Where(x => x.Attendees.Any(a => a.Username == _userAccessor.GetUsername())); // aktualnie zalogowany użytkownik
            }

            if (request.Params.IsHost && !request.Params.IsGoing)
            {
                query = query.Where(x => x.HostUsername == _userAccessor.GetUsername()); // aktualnie zalogowany użytkownik
            }



            // var activitiesToReturn = _mapper.Map<List<ActivityDto>>(activities);
            return Result<PagedList<ActivityDto>>.Success(
                await PagedList<ActivityDto>.CreateAsync(query,request.Params.PageNumber,
                    request.Params.PageSize)
            );
        }
    }
}
