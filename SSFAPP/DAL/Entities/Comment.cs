using System;

namespace SSFAPP.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Topic Topic { get; set; }
        public int TopicId { get; set; }
        public String Content { get; set; }
        public User WrittenByUser { get; set; }
    }
}