using System;

namespace DatingApp.API.Models.UserAgg.Dependency
{
    public class Photo
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
    }
}