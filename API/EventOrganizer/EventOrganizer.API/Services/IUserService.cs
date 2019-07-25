using EventOrganizer.Model;
using System;

namespace EventOrganizer.API.Services
{
    public interface IUserService : IDisposable
    {
        User Authenticate(string username, string password);
        string EncryptPassword(string password);
    }
}
