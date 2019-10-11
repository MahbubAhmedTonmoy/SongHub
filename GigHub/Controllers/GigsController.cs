using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly  ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending()
        {
            var UserId = User.Identity.GetUserId();
            var gigs = _context.Attends
                .Where(a => a.AttendeeId == UserId)
                .Select(a => a.Gig)
                .Include(a=>a.Artist).Include(g=>g.Genre).ToList();

            var ViewModel = new HomeViewModel
            {
                UpcommingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
				Heading = "Event I am Attending"
            };

            return View("Gigs", ViewModel);
        }
		
		
		[HttpPost]
		public ActionResult Search(HomeViewModel ViewModel)
		{
			return RedirectToAction("Index", "Home", new { query = ViewModel.SearchTerm });
		}			
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Heading = "Create a Event",
                Geners = _context.Genres.ToList()
            };
            return View("GigForm", viewModel);
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewmodel)
        {

            //// comment cz  foreignKey add in model
            //var artistId = User.Identity.GetUserId();
			// 2 query in db
            //var artist = _context.Users.Single(u => u.Id == artistId); // find id which login
            // var genre = _context.Genres.Single(u => u.Id == viewmodel.Genre);
            
			if (!ModelState.IsValid)
            {
                viewmodel.Geners = _context.Genres.ToList(); // gig form e drop down e null value er jonno
                return View("GigForm", viewmodel);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = viewmodel.Venue,
                DateTime = viewmodel.GetDateTime(),
                GenreId = viewmodel.Genre
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Mine","Gigs");
        }

        [Authorize]
        public ActionResult Mine() // my up commimg gig
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs.
                Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre).
                ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Heading = "edit a Event",
                Id = gig.Id,
                Geners = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };
          
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewmodel)
        {
           
            if (!ModelState.IsValid)
            {
                viewmodel.Geners = _context.Genres.ToList();
                return View("GigForm", viewmodel);// if not valid return same view
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == viewmodel.Id && g.ArtistId == userId);

            gig.Venue = viewmodel.Venue;
            gig.DateTime = viewmodel.GetDateTime();
            gig.GenreId = viewmodel.Genre;

            
            _context.SaveChanges();
            return RedirectToAction("Mine", "Gigs");
        }
    }
}