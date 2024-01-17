using Application.Core;
using Domain;
using MediatR;
using System;

namespace Application.Activities.Queries
{
    public class GetActivityById :IRequest<Result<ActivityDto>>
    {
        public Guid Id { get; set; }
    }
}
