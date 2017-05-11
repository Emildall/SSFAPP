using SSFAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SSFAPP.DAL
{
    public class InitializeDb : DropCreateDatabaseAlways<SSFContext>
    {
        protected override void Seed(SSFContext context)
        {
            List<Fish> Fishes = new List<Fish>();
            List<User> Users = new List<User>();
            List<Topic> Topics = new List<Topic>();
            List<Comment> Comments = new List<Comment>();

            Comment c1 = new Comment() { Content= "hey guys, see my long fish", Date=DateTime.Now, Topic=null, WrittenByUser=null};
            Comment c2 = new Comment() { Content = "wow its long", Date = DateTime.Now, Topic = null, WrittenByUser = null };
            Comment c3 = new Comment() { Content = "yes i dink so to", Date = DateTime.Now, Topic = null, WrittenByUser = null };
            Comment c4 = new Comment() { Content = "its long as my dick", Date = DateTime.Now, Topic = null, WrittenByUser = null };
            Comment c5 = new Comment() { Content = "no its not anders", Date = DateTime.Now, Topic = null, WrittenByUser = null };
            Comment c6 = new Comment() { Content = "yes im agmam", Date = DateTime.Now, Topic = null, WrittenByUser = null };
            Comments.Add(c1);
            Comments.Add(c2);
            Comments.Add(c3);
            Comments.Add(c4);
            Comments.Add(c5);
            Comments.Add(c6);
            Topic topic = new Topic() { Comments = Comments, Header="Trout discussion", Date=DateTime.Now };
            Topic topic2 = new Topic() { Comments = new List<Comment>(), Header = "Salmon discussion", Date = DateTime.Now };



            User Esben = new User()
            {
                Id = 1,
                Email = "Esben.laursen@gmail.com",
                FishList = null,
                Username = "ebbe",
                Name = "Esben",
                Password = "lol123",
                PhoneNr = "324940283"
            };
            User Emil = new User()
            {
                Id = 2,
                Email = "Emilogefternavnmor@gmail.com",
                FishList = null,
            
                Name = "Emil",
                Password = "lol123",
                PhoneNr = "314242342"
            };
            Users.Add(Esben);
            Users.Add(Emil);
            foreach (Comment c in Comments)
            {
                c.Topic = topic;
                c.WrittenByUser = Esben;
            }

            topic.WrittenByUser = Emil;

            Topics.Add(topic);

            topic2.WrittenByUser = Esben;

            Topics.Add(topic2);


            Fish f = new Fish()
            {
                id = 1,
                Type = "Salmon",
                Bait = "Flyfishing",
                CaughtByUser = Esben,
                DayCaught = DateTime.Now,
                Length = 50.0,
                Location = "Bathroom",
                Weight = 20.0
            };
            Fish f2 = new Fish()
            {
                id = 1,
                Type = "Trout",
                Bait = "Flyfishing",
                CaughtByUser = Emil,
                DayCaught = DateTime.Now.AddDays(-3),
                Length = 100.0,
                Location = "In toilet",
                Weight = 5.0
            };
            Fishes.Add(f);
            Fishes.Add(f2);


            foreach (User u in Users)
            {
                context.Users.Add(u);
            }
            foreach (Fish fish in Fishes)
            {
                context.Fishes.Add(fish);
            }
            foreach (Topic t in Topics)
            {
                context.Topics.Add(t);
            }
            

            foreach (Comment c in Comments)
            {
                context.Comments.Add(c);
            }

            context.SaveChanges();


        }
    }
}