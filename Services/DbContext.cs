using MediaItemsServer.Helpers;
using MediaItemsServer.Models;

namespace MediaItemsServer.Services
{
    public sealed class DbContext
    {
        public IList<UserRole> Roles => new List<UserRole>
        {
            new UserRole { Id = 1, RoleName = Consts.Administrator },
            new UserRole { Id = 2, RoleName = Consts.User }
        };

        public IList<User> Users => new List<User>
        {
            new User { Id = 1, Name = "admin", Email = "admin@admin.com", Password = "admin" },
            new User { Id = 2, Name = "test", Email = "test@test.com", Password = "test" }
        };

        public IList<SecurityRelation> SecurityRelations => new List<SecurityRelation>
        {
            new SecurityRelation { ParentId = 1, ChildId = 1, InclusionType = InclusionType.UserInRole },
            new SecurityRelation { ParentId = 2, ChildId = 1, InclusionType = InclusionType.UserInRole },
            new SecurityRelation { ParentId = 2, ChildId = 2, InclusionType = InclusionType.UserInRole }
        };
    }
}
