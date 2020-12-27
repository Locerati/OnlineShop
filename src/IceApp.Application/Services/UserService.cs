using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using IceApp.Application.Interfaces;
using IceApp.Domain.ChildModels;

namespace IceApp.Application.Services
{
    public class UserService:IUserService
    {
        public IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserModel> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        public Task<UserIdentity> GetUserIdentity(string email)
        {
            return _userRepository.GetUserIdentity(email);
        }
        public int AddUser(UserModel user)
        {
            return _userRepository.AddUser(user);
        }
        public void AddIdentity(UserIdentity identity)
        {
             _userRepository.AddIdentity(identity);
        }
        public Role GetRoleByName(string name)
        {
            return _userRepository.GetRoleByName(name);
        }
        public Role GetRoleById(int id)
        {
            return _userRepository.GetRoleById(id);
        }
        public Task<int> GetUserIdByEmail(string email)
        {
            return _userRepository.GetUserIdByEmail(email);
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        public void UpdateImage(byte[] image, int userid)
        {
              _userRepository.UpdateImage(image,userid);
        }
        public void UpdateWithoutImage(UserModel user)
        {
            _userRepository.UpdateWithoutImage(user);
        }
    }
}
