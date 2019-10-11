using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.DTO
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        public GigDTO Gig { get; private set; }
    }
}