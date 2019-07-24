﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.API.Services;
using EventOrganizer.Model;
using EventOrganizer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public User ConnectUser(string username, string password)
        {
            using (IRepository<User> userRepository = new UserRepository(_context))
            {
                IUserService userService = new UserService(userRepository);
                return userService.Authenticate(username, password);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<User> RegisterUser([FromBody]User user)
        {
            using (IRepository<User> userRepository = new UserRepository(_context))
            {
                var verifyUser = userRepository.GetAll().FirstOrDefault(u => u.Username == user.Username);
                if (verifyUser != null)
                    return null;
                var added = await userRepository.Insert(user);
                if (!added) return null;
                IUserService userService = new UserService(userRepository);
                return userService.Authenticate(user.Username, user.Password);
            }
        }

        [HttpGet("getAll")]
        public IEnumerable<User> GetAllUsers()
        {
            using (IRepository<User> userRepository = new UserRepository(_context))
            {
                return userRepository.GetAll().Select(
                    x=> {
                        x.Password = null;
                        return x;
                    }).ToList();
            }
        }

    }
}