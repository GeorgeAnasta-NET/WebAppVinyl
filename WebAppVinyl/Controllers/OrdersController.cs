using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppVinyl.Models;
using System.Data.Entity;

namespace WebAppVinyl.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext context;

        public OrdersController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();

            

            return View();
        }
    }
}