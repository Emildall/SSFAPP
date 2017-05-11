using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SSFAPP.DAL;
using SSFAPP.DAL.Entities;
using SSFAPP.Models;

namespace SSFAPP.Controllers
{
    public class UsersController : ApiController
    {
        private SSFContext db = new SSFContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users.Include(x=> x.FishList);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Include(x => x.FishList).FirstOrDefault(x=> x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(db.Users.Count(x=> x.Username == user.Username) ==0)
            {
                db.Users.Add(user);
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var userforauth = new ApplicationUser();
                userforauth.UserName = user.Username;
                userforauth.Email = user.Email;
                string pass = user.Password;

                var succes = userManager.Create(userforauth, pass);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            return BadRequest("Username already in use");
           
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}