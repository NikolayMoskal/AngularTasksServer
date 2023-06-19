using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;

namespace MediaItemsServer.Services
{
    public class UserService : IUserService
    {
        // Instead of database table
        private static readonly IList<User> Users = new List<User>
        {
            new User { Name = "admin", Email = "admin@admin.com", Password = "admin" }
        };
        private static readonly object Lock = new();

        public User Get(string userName)
        {
            return Users.FirstOrDefault(x => x.Name == userName);
        }

        public void SaveOrUpdate(User user)
        {
            lock (Lock)
            {
                if (Users.Any(x => x.Name == user.Name))
                {
                    var existing = Users.First(x => x.Name == user.Name);
                    Users.Remove(existing);
                }
                Users.Add(user);
            }
        }

        public void Delete(string userName)
        {
            lock (Lock)
            {
                if (Users.Any(x => x.Name == userName))
                {
                    var existing = Users.First(x => x.Name == userName);
                    Users.Remove(existing);
                }
            }
        }
    }
}
