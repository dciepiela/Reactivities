﻿using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class UpdateAttendanceHandler : IRequestHandler<UpdateAttendance, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public UpdateAttendanceHandler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(UpdateAttendance request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
                .Include(activity => activity.Attendees)
                .ThenInclude(actAttendee => actAttendee.AppUser)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

            if(activity == null)
            {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUsername());

            if (user == null)
            {
                return null;
            }

            var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

            var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

            if(attendance != null && hostUsername == user.UserName)
            {
                activity.IsCancelled = !activity.IsCancelled;
            }

            if (attendance != null && hostUsername != user.UserName)
            {
                activity.Attendees.Remove(attendance);
            }

            if(attendance == null)
            {
                attendance = new ActivityAttendee
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                };
                activity.Attendees.Add(attendance);
            }

            var result = await _context.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance!");

        }
    }
}
