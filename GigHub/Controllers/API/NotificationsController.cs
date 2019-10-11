using AutoMapper;
using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        
        public IEnumerable<NotificationDTO> GetNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig).ToList();

            // pass mapping into App_start -> MappingProfile

            return notifications.Select(Mapper.Map<Notification,NotificationDTO>);

        }
		
		[HttpPost]
		public IHttpActionResult MarkAsRead()
		{
			var userId = User.Identity.GetUserId();
			var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead).ToList();
			
			notifications.ForEach(n => n.Read());
			/*
			mark all list of noti is read by using Read() method ->
			which is emplement in UserNotifications class
			*/ 
			_context.SaveChanges();
			
			return Ok();
		}
        
    }
}
