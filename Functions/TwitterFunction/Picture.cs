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
namespace EventOrganizer.Model
{
    public class Entity
    {
        public long Id { get; set; }
    }
}
namespace EventOrganizer.Model
{
    public class UserPicture : Entity
    {
        public long IdFromPlat { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string UserProfilePictureUrl { get; set; }
    }
}
