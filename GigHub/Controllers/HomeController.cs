using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        
        public HomeController(){
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var upcommingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
				
			if(!String.IsNullOrWhiteSpace(query))
			{
				upcommingGigs = upcommingGigs
					.Where(g => g.Artist.Name.Contains(query) ||
								g.Genre.Name.Contains(query) ||
								g.Venue.Contains(query));
			}				
		    
            var viewModel = new HomeViewModel
            {
                UpcommingGigs = upcommingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcomming Events",
				SearchTerm = query
            };
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}