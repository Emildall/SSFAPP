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

namespace SSFAPP.Controllers
{
    public class TopicsController : ApiController
    {
        private SSFContext db = new SSFContext();

        // GET: api/Topics
        public IQueryable<Topic> GetTopics()
        {
            return db.Topics.Include(t => t.Comments);
        }

        // GET: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult GetTopic(int id)
        {
            Topic topic = db.Topics.Include(t => t.Comments).Include(w => w.WrittenByUser).FirstOrDefault(t => t.Id == id);
           
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
        }

        // PUT: api/Topics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTopic(int id, Topic topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != topic.Id)
            {
                return BadRequest();
            }

            db.Entry(topic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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
        // POST: api/Topics
        [ResponseType(typeof(Topic))]
        public IHttpActionResult PostTopic(Topic topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


           // db.Entry(topic.Comments).State = EntityState.Unchanged;
            db.Entry(topic.WrittenByUser).State = EntityState.Unchanged;

            Topic t = db.Topics.Add(topic);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = topic.Id }, t);
        }

        // DELETE: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult DeleteTopic(int id)
        {
             Topic topic = db.Topics.Include("Comments").FirstOrDefault(t => t.Id == id);
           // Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return NotFound();
            }
            var comments = from c in db.Comments
                        where c.TopicId == id //this is foreing key
                        select c;

            foreach (Comment  cc in comments)
            {
                db.Comments.Remove(cc);

            }

              //  db.Entry(c).State = EntityState.Deleted;
            
          //  db.SaveChanges();
            

            db.Topics.Remove(topic);
            db.SaveChanges();

            return Ok(topic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TopicExists(int id)
        {
            return db.Topics.Count(e => e.Id == id) > 0;
        }
    }
}