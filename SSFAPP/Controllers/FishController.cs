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
using SSFAPP.DAL;
using SSFAPP.DAL.Entities;
using System.Web.Http.Cors;
using System.Web;

namespace SSFAPP.Controllers
{
    public class FishController : ApiController
    {
        private SSFContext db = new SSFContext();

        // GET: api/Fish
        public IQueryable<Fish> GetFishes()
        {
            return db.Fishes.Include(x => x.CaughtByUser);
        }

        // GET: api/Fish/5
        [ResponseType(typeof(Fish))]
        public HttpResponseMessage GetFish(int id)
        {
            Fish fish = db.Fishes.Include(x => x.CaughtByUser).FirstOrDefault(x => x.id == id);
            if (fish == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var resp = Request.CreateResponse(HttpStatusCode.OK, fish);
            return resp;
        }

        // PUT: api/Fish/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFish(int id, Fish fish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fish.id)
            {
                return BadRequest();
            }

            db.Entry(fish).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishExists(id))
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

        [Authorize]
        // POST: api/Fish
        [ResponseType(typeof(Fish))]
        public IHttpActionResult PostFish(Fish fish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(fish.CaughtByUser).State = EntityState.Unchanged;

            db.Fishes.Add(fish);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fish.id }, fish);
        }

        // DELETE: api/Fish/5
        [ResponseType(typeof(Fish))]
        public IHttpActionResult DeleteFish(int id)
        {
            Fish fish = db.Fishes.Find(id);
            if (fish == null)
            {
                return NotFound();
            }

            db.Fishes.Remove(fish);
            db.SaveChanges();

            return Ok(fish);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FishExists(int id)
        {
            return db.Fishes.Count(e => e.id == id) > 0;
        }
    }
}