using MediaItemsServer.Helpers;
using MediaItemsServer.Models;

namespace MediaItemsServer.Services
{
    public sealed class DbContext
    {
        public IList<UserRole> Roles => new List<UserRole>
        {
            new UserRole { Id = 1, RoleName = Consts.AdministratorRole }
        };

        public IList<User> Users => new List<User>
        {
            new User { Id = 1, Name = "admin", Email = "admin@admin.com", Password = "admin" }
        };

        public IList<SecurityRelation> SecurityRelations => new List<SecurityRelation>
        {
            new SecurityRelation { ParentId = 1, ChildId = 1, InclusionType = InclusionType.UserInRole }
        };
    }
}
