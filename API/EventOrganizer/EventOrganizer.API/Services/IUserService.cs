using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Model;

namespace EventOrganizer.API.Services
{
    public interface IUserService:IDisposable
    {
        User Authenticate(string username, string password);
    }
}
