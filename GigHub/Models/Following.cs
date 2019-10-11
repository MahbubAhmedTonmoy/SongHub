using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Following
    {
        public ApplicationUser  Follower { get; set; }

        public ApplicationUser Followee { get; set; } // artist k follow korbo

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }
    }

    // add collection in AppUser and initialize in constructor [Followees] [Followers]
}
