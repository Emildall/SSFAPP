using System;

namespace SSFAPP.DAL.Entities
{
    public class Fish
    {
        public int id { get; set; }
        public String Type { get; set; }
        public User CaughtByUser { get; set; }
        public DateTime DayCaught { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public String Bait { get; set; }
        public String Location { get; set; }
    }
}