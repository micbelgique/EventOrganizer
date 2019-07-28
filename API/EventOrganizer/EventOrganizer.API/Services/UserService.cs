using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EventOrganizer.API.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public User Authenticate(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("hMlBevhXPdc9ucEZYcxyMmZ2p11RWGteFbH56YYRoAsvGKHkAS3-Tqg_nPNA9S9V_OZE1XqTLQRNWwGc1roEtd-NatZI6AJ1tHXfQnpMJZiUW8FQKF4il2Km9Im3raVnk5A9G1l6r51C-4YsCUGrRA1oamJFvrmTe3rh2Z0OoB6L2xS9hRnw9p3US939JY7LH_zh3NhwJ3o2D91TlxrLgaCLEy0pnHfL0PItTLAb1fFnVb6OBwp33ICTWfR617ozyb6Bgvr7jhqtY_OrvsnPmGFLuhrnzUqUJNbL37zyPtbvUxMM0S1rtSwVh700fGVhKSQYbIOkl23vrk4dR2DwlQ");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            user.Password = null;
            if (user.UserTeam != null)
                user.UserTeam.Users = null;
            return user;

        }

        public string EncryptPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }
        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
