using SSFAPP.DAL;
using SSFAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SSFAPP.Controllers
{
    public class UserByUsernameController : ApiController
    {
        private SSFContext db = new SSFContext();
        // GET: api/UserByUsername/Username
        [ResponseType(typeof(int))]
        public IHttpActionResult GetUser(string Username)
        {
            User user = db.Users.FirstOrDefault(x => x.Username.Equals(Username));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Id);
        }
    }
}
