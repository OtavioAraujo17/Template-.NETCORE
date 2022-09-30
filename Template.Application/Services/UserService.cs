using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Auth.Services;
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
            if (userViewModel.Id != Guid.Empty)
                throw new Exception("UserID must be empty!");

            Validator.ValidateObject(userViewModel, new ValidationContext(userViewModel), true);

            User user = mapper.Map<User>(userViewModel);
            //user.Password = EncryptPassword(user.Password);
            this.userRepository.Create(user);
            return true;
        }

        public UserViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid!");

            User user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if(user == null)
                throw new Exception("User not found!");

            return mapper.Map<UserViewModel>(user);
        }

        public bool Put(UserViewModel userViewModel)
        {
            if (userViewModel.Id == Guid.Empty)
                throw new Exception("ID is invalid");


            User user = this.userRepository.Find(x => x.Id == userViewModel.Id && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found!");

            user = mapper.Map<User>(userViewModel);
            //_user.Password = EncryptPassword(_user.Password);
            this.userRepository.Update(user);
            return true;
        }

        public bool Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid!");

            User user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found!");

             return this.userRepository.Delete(user);
        }

        public UserAuthenticateResponseViewModel Authentication(UserAuthenticateRequestViewModel users)
        {
            if (string.IsNullOrEmpty(users.Email) || string.IsNullOrEmpty(users.Password))
                throw new Exception("Email/Password are required.");

            //user.Password = EncryptPassword(user.Password);

            User user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToLower() == users.Email.ToLower());
            if(user == null)
                throw new Exception("User not found!");

            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(user), TokenService.GenerateToken(user));
        }
    }
}
