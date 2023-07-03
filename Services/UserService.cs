using MediaItemsServer.Data.Contracts;
using MediaItemsServer.Models;
using MediaItemsServer.Services.Contracts;

namespace MediaItemsServer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Get(string userName)
        {
            return _userRepository.Get(userName);
        }

        public void Save(User user)
        {
            _userRepository.Save(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void SaveOrUpdate(User user)
        {
            _userRepository.SaveOrUpdate(user);
        }

        public void Delete(string userName)
        {
            _userRepository.Delete(userName);
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            return _userRepository.IsUserInRole(userName, roleName);
        }
    }
}
