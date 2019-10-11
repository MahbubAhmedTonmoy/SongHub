 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; } // id is a key not change after once created 

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }
		
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get; private set; } // each notification have 1 gig 

        protected Notification() { }
		
        public Notification(NotificationType type,Gig gig)
        {
            if( gig == null)
                throw new ArgumentNullException("gig");
            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }
    }

    //association class between user and notification
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; } 

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; } // not set after initalize
        public Notification Notification { get; private set; } // not set after initalize

        public bool IsRead { get; private set; }

        protected UserNotification() { } //default constructor for entity framework cz o create custom constructor

        public UserNotification (ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");
            User = user;
            Notification = notification;
        }
		
		public void Read()
		{
			IsRead = true;
		}
    }

    public enum NotificationType
    {
        GigCanceled = 1,
        GigUpdate = 2,
        GigCreate = 3
    }
}