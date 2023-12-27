using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Commands
{
    public class CreateActivity : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}
