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
