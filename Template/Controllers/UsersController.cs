﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Application.Interfaces;
using Template.Application.ViewModels;

namespace Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userService.Get());
        }

        [HttpPost]
        public IActionResult Post(UserViewModel userViewModel)
        {
            return Ok(userService.Post(userViewModel));
        }
    }
}