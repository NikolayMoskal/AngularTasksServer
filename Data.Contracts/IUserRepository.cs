﻿using MediaItemsServer.Models;

namespace MediaItemsServer.Data.Contracts
{
    public interface IUserRepository
    {
        User Get(string userName);
        void Save(User user);
        void Update(User user);
        void SaveOrUpdate(User user);
        void Delete(string userName);
        bool IsUserInRole(string userName, string roleName);
    }
}
