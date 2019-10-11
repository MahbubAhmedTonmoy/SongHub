using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    public class FolloingDto
    {
        public string FolloweeId { get; set; }
    }

    [Authorize]
    public class FollowingController : ApiController
    {
        public readonly ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }
        
        [HttpPost]
        public IHttpActionResult Follow(FolloingDto dto)
        {
            var userId = User.Identity.GetUserId();

            //prevent if once follow
            var alreadyFollow = _context.Followings.Any(f => f.FolloweeId ==userId 
            && f.FolloweeId == dto.FolloweeId);
            if (alreadyFollow)
            {
                return BadRequest("you already follow");
            }

            var follwing = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(follwing);
            _context.SaveChanges();

            return Ok();
;        }

    }
}
