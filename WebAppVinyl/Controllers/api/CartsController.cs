using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppVinyl.Dtos;
using WebAppVinyl.Models;

namespace WebAppVinyl.Controllers.api
{
    public class CartsController : ApiController
    {
        private ApplicationDbContext context;

        public CartsController()
        {
            context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult Cart(CartDto dto)
        {
            //what if attendance already exist??
            var userId = User.Identity.GetUserId();
            //var exists = context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId);
            if (context.Carts.Any(c => c.BuyerId == userId && c.VinylId == dto.VinylId))
                return BadRequest("Cart already exists");

            var cart = new Cart
            {
                VinylId = dto.VinylId,
                BuyerId = userId
                //Quantity = dto.Vinyl.Quantity

            };
            //add object to attendances DbSet

            context.Carts.Add(cart);
            //save changes to db
            context.SaveChanges();
            
            return Ok();
        }

        [HttpDelete]
        public void DeleteFromCart(int id)
        {
            var userId = User.Identity.GetUserId();

            var vinylNotWanted = context.Carts
                .Where(c => c.VinylId == id && c.BuyerId == userId)
                .SingleOrDefault();

            context.Carts.Remove(vinylNotWanted);
            context.SaveChanges();
            
        }
    }
}
