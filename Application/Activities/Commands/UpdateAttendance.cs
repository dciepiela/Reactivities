using Application.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class UpdateAttendance : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
