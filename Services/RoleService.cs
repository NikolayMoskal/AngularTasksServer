using MediaItemsServer.Data.Contracts;
using MediaItemsServer.Models;
using MediaItemsServer.Services.Contracts;

namespace MediaItemsServer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IList<UserRole> GetRolesForUser(string userName)
        {
            return _roleRepository.GetRolesForUser(userName);
        }

        public void Save(UserRole userRole)
        {
            _roleRepository.Save(userRole);
        }

        public void Delete(UserRole userRole)
        {
            _roleRepository.Delete(userRole);
        }
    }
}
