﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Domain.Entities;
using Template.Domain.Interfaces;

namespace Template.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public List<UserViewModel> Get()
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            IEnumerable<User> users = this.userRepository.GetAll();

            userViewModels = mapper.Map<List<UserViewModel>>(users);

            //foreach (var item in users)
            //    userViewModels.Add(mapper.Map<UserViewModel>(item));


                //{
                //    Id = item.Id,
                //    Email = item.Email,
                //    Name = item.Name
                //});
            return userViewModels;
        }

        public bool Post(UserViewModel userViewModel)
        {
            //User user = new User
            //{
            //    Id = Guid.NewGuid(),
            //    Email = userViewModel.Email,
            //    Name = userViewModel.Name
            //};

            User user = mapper.Map<User>(userViewModel);

            this.userRepository.Create(user);
            return true;
        }
    }
}