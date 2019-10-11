using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
    public class AttendanceDto
    {
        public int GigId { get; set; }
    }
   
    [Authorize]

    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
			
			// prevent duplicate
            var exists = _context.Attends.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId); 

            if(exists){
                return BadRequest("the attendance already exist");
            }
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attends.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
