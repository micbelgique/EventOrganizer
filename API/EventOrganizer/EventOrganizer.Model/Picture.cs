using System;

namespace EventOrganizer.Model
{
    public class Picture : Entity
    {
        public long IdFromPlat { get; set; }
        public string PictureUrl { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Removed { get; set; } = false;
        public UserPicture User { get; set; }
    }
}
