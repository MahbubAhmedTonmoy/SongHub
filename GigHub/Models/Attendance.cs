using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
	// many to many relation (gig)*----*(user)
    public class Attendance
    {
        public Gig Gig { get; set; }

        public ApplicationUser Attendee { get; set; }
		
		// Foreign Key property 
		
		// combination od gigId and attendeeId represent Attendance this will be primary key
        [Key] // composite primary key
        [Column(Order = 1)]
        public int GigId { get; set; }

        [Key] // composite primary key
        [Column(Order = 2)]
        public string AttendeeId { get; set; }

    }
}