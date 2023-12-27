using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Commands
{
    public class EditActivity:IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}
