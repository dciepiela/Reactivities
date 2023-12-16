using Domain;
using MediatR;

namespace Application.Activities.Commands
{
    public class EditActivity:IRequest
    {
        public Activity Activity { get; set; }
    }
}
