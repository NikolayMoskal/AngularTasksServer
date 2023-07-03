using MediaItemsServer.Data.Contracts;
using MediaItemsServer.Models;

namespace MediaItemsServer.Data
{
    public class UserRepository : IUserRepository
    {
        private static readonly object Lock = new();
        private readonly DbContext _dbContext;
        private readonly IRoleRepository _roleRepository;

        public UserRepository(DbContext dbContext, IRoleRepository roleService)
        {
            _dbContext = dbContext;
            _roleRepository = roleService;
        }

        private IList<User> Users => _dbContext.Users;

        public User Get(string userName)
        {
            return Users.FirstOrDefault(x => x.Name == userName);
        }

        public void Save(User user)
        {
            lock (Lock)
            {
                if (Users.Any(x => x.Name.Equals(user.Name, StringComparison.InvariantCultureIgnoreCase)))
                    return;

                AddUser(user);
            }
        }

        public void Update(User user)
        {
            lock (Lock)
            {
                if (Users.Any(x => x.Name.Equals(user.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Users.ToList().RemoveAll(x => x.Name.Equals(user.Name, StringComparison.InvariantCultureIgnoreCase));
                }

                AddUser(user);
            }
        }

        public void SaveOrUpdate(User user)
        {
            lock (Lock)
            {
                if (Users.Any(x => x.Name == user.Name))
                {
                    Update(user);
                }
                else
                {
                    Save(user);
                }
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

        private int AddUser(User user)
        {
            var id = Users.Count;
            user.Id = id;
            Users.Add(user);

            return id;
        }
    }
}
