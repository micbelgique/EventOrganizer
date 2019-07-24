namespace EventOrganizer.Model
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public virtual Team UserTeam { get; set; }
    }
}
