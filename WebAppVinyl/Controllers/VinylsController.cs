using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppVinyl.Models;
using System.Data.Entity;
using WebAppVinyl.ViewModels;
using Microsoft.AspNet.Identity;

namespace WebAppVinyl.Controllers
{
    public class VinylsController : Controller
    {
        private ApplicationDbContext context;

        public VinylsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Vinyls
        public ActionResult Index()
        {
            var vinyls = context.Vinyls
                .Include(v => v.Genre)
                .Include(l => l.Label)
                .ToList();

            return View("Vinyls",vinyls);
        }

        public ActionResult Create()
        {
            var viewModel = new VinylFormViewModel()
            {
                Genres = context.Genres.ToList(),
                Labels = context.Labels.ToList()
            };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var vinyl = context.Vinyls.SingleOrDefault(v => v.Id == id);
            var genre = context.Genres.ToList();
            var label = context.Labels.ToList();

            if (vinyl == null)
            {
                return HttpNotFound();
            }

            var vinylModel = new VinylFormViewModel()
            {
                Id = (int)vinyl.Id,
                Title = vinyl.Title,
                Artist = vinyl.Artist,
                Genre = (byte)vinyl.GenreId,
                ReleaseYear = vinyl.ReleaseYear,
                Label = (byte)vinyl.LabelId,
                Price = vinyl.Price,
                Genres = genre,
                Labels = label
            };

            return View("VinylForm", vinylModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(VinylFormViewModel vinylModel)
        {
            if (!ModelState.IsValid)
            {
                var musicModel = new VinylFormViewModel()
                {
                    Genres = context.Genres.ToList(),
                    Labels = context.Labels.ToList()
                };
                return View("VinylForm", musicModel);
            }

            if (vinylModel.Id == 0)  // Create
            {
                var vinyl = new Vinyl()
                {
                    Id = vinylModel.Id,
                    Title = vinylModel.Title,
                    Artist = vinylModel.Artist,
                    ReleaseYear = vinylModel.ReleaseYear,
                    Price = vinylModel.Price,
                    GenreId = (int)vinylModel.Genre,
                    LabelId = (int)vinylModel.Label
                };
                context.Vinyls.Add(vinyl);
            }
            else  //Edit
            {
                var vinylDb = context.Vinyls.SingleOrDefault(v => v.Id == vinylModel.Id);
                // autes oi tesseris grammes mporeis na peis oti einai i whiteList
                vinylDb.Title = vinylModel.Title;
                vinylDb.Artist = vinylModel.Artist;
                vinylDb.ReleaseYear = vinylModel.ReleaseYear;
                vinylDb.GenreId = vinylModel.Genre;
                vinylDb.LabelId = vinylModel.Label;
                vinylDb.Price = vinylModel.Price;
            }
            context.SaveChanges();

            return RedirectToAction("Index", "Vinyls");
        }
        
        //fluent Api gia ti sxesi genre me vinyl
        // future task
        public ActionResult Delete(int id)
        {
            var vinyl = context.Vinyls
                .Where(v => v.Id == id)
                .Include(g => g.Genre)
                .Include(l => l.Label)
                .SingleOrDefault();
                
            context.Vinyls.Remove(vinyl);
            context.SaveChanges();

            return RedirectToAction("Index", "Vinyls");
        }

        // FUture Task : diffirent view
        // More Details about Groups
        public ActionResult Details(int id)
        {
            IEnumerable<Vinyl> vinyl = context.Vinyls
                .Where(v => v.Id == id)
                .Include(g => g.Genre)
                .Include(l => l.Label);

            return View("Vinyls", vinyl);
        }

        [Authorize]
        public ActionResult MyCart()
        {
            var userId = User.Identity.GetUserId();
            // ksekinaw meso attendee giati apo ekei mporw na parw ton user kai ti sinaulia
            // kai ola ta ipoloipa einai auta pou exei mesa to gig epeidi thelw onomata tou kathe property
            // den vazw Id giati thelw na deiksw ta onomata opote prepei na parw ta object
            var vinylToBuy = context.Carts
                .Where(c => c.BuyerId == userId)
                .Select(c => c.Vinyl)
                .Include(c => c.Genre)
                .Include(c => c.Label)
                .ToList();

            var viewModel = new VinylsViewModel
            {
                VinylToBuy = vinylToBuy,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Vinyls I am Attending"
            };

            return View("MyCart",viewModel);
        }
    }
}