using Domain;
using MediatR;
using System;

namespace Application.Activities.Queries
{
    public class GetActivityById :IRequest<Activity>
    {
        public Guid Id { get; set; }
    }
}
