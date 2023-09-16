using API.Helpers;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitiOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<User> passwordHasher)
        {
            _unitiOfWork = unitOfWork;
            _jwt = jwt.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> RegisterAsync(UserDto dto)
        {
            User user = new User()
            {
                Email = dto.Email
            };

            user.Password = _passwordHasher.HashPassword(user,dto.Password);

            var ExistentUser = _unitiOfWork.UserRepository.GetByIdAsync(dto.Email);

            //If the email is not repeated, u can create the user
            if (ExistentUser == null)
            {

            }

        }
    }
}
