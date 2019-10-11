using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }


        public bool IsCanceled { get; private set; } //Artist Cancle her event not delete
       
        public ApplicationUser Artist { get; set; } //navigation property

        [Required]
        public string ArtistId { get; set; }// foreign key prop

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        
        public Genre Genre { get; set; }//navigation property 

        [Required]
        public byte GenreId { get; set; }// foreign key prop

        public ICollection<Attendance> Attendances {get; private set;}

        public Gig(){
            Attendances = new Collection<Attendance>();
        }
		
        public void Cancel()
        {
            this.IsCanceled = true;

            var notification = new Notification(NotificationType.GigCanceled, this);
           
            foreach(var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
   
}