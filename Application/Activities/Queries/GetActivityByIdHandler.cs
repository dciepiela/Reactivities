using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Queries
{
    public class GetActivityByIdHandler:IRequestHandler<GetActivityById, Result<ActivityDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetActivityByIdHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ActivityDto>> Handle(GetActivityById request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<ActivityDto>.Success(activity);
        }
    }
}
