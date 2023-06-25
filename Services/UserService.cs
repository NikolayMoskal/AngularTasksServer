using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;

namespace MediaItemsServer.Services
{
    public class UserService : IUserService
    {
        private static readonly object Lock = new();
        private readonly DbContext _dbContext;

        public UserService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IList<User> Users => _dbContext.Users;

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
                    Users.ToList().RemoveAll(x => x.Name == user.Name);
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
                    Users.ToList().RemoveAll(x => x.Name == userName);
                }
            }
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            lock (Lock)
            {
                return Users.Where(x => x.Name == userName)
                    .Join(_dbContext.SecurityRelations, user => user.Id, relation => relation.ChildId,
                        (user, relation) => relation)
                    .Join(_dbContext.Roles.Where(x => x.RoleName == roleName), relation => relation.ParentId,
                        role => role.Id, (relation, role) => role)
                    .Any();
            }
        }
    }
}
