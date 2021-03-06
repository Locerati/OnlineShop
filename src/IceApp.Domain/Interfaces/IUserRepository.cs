﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;
namespace IceApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserModel> GetUserById(int id);
        Task<UserIdentity> GetUserIdentity(string email);
        public int AddUser(UserModel user);
        public void AddIdentity(UserIdentity identity);
        public Role GetRoleByName(string name);
        public Role GetRoleById(int id);
        public Task<int> GetUserIdByEmail(string email);
        public Task<UserModel> GetUserByEmail(string email);
        public  void UpdateImage(byte[] image, int userid);
        public void UpdateWithoutImage(UserModel user);
    }
}
