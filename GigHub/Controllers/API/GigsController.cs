using GigHub.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancle(int id)
        {
            // 1. find gig id and user who want to attand
            // 2 check it is already canceled
            // 3. send notification all people for this gig


            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
							.Include(g => g.Attendances.Select(a => a.Attendee))
							.Single(g => g.Id == id && g.ArtistId == userId);

            if(gig.IsCanceled)
            {
                return NotFound();
            }

			
            gig.Cancel(); // send notification - see in gig class

            _context.SaveChanges();

            return Ok();
			
			/*
			var notification = new Notifaction // pass this in class as contructor 
			{
				DateTime = DateTime.Now,
				Gig = gig,
				Type = NotificationType.GigCanceled
			};
			// this pass to include in upper 
			var attandees = _context.Attends
				.where(a=>a.GigId == gig.Id)
				.Select(a=> a.Attendee).TOList();
			
			foreach(var attandee in attandees)
			{
				attandee.Notify(notification);
				//pass this to Application user
				var userNotification = new UserNotification
				{
					User = Attendee,
					Notification = notification
				};
				_context.UserNotifications.Add(userNotification);
			}
			
			//gig.IsCanceled = true; // 1st do this
			
			var notification = new Notifaction( NotificationType.GigCanceled,gig);
			foreach(var attandee in gig.Attendances.Select(a => a.Attendee))

				attandee.Notify(notification);
			}
			
			*/

        }
    }
}
