using Domain;
using MediatR;

namespace Application.Activities.Commands
{
    public class CreateActivity : IRequest
    {
        public Activity Activity { get; set; }
    }
}
