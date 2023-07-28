using MediaItemsServer.Models;

namespace MediaItemsServer.Data
{
    public sealed class DbContext
    {
        public List<UserRole> Roles { get; } = new()
        {
            new UserRole { Id = 1, RoleName = "administrator" },
            new UserRole { Id = 2, RoleName = "user" }
        };

        public List<User> Users { get; } = new()
        {
            new User { Id = 1, Name = "admin", Email = "admin@admin.com", Password = "admin" },
            new User { Id = 2, Name = "test", Email = "test@test.com", Password = "test" }
        };

        public List<SecurityRelation> SecurityRelations { get; } = new()
        {
            new SecurityRelation { ParentId = 1, ChildId = 1, InclusionType = InclusionType.UserInRole },
            new SecurityRelation { ParentId = 2, ChildId = 1, InclusionType = InclusionType.UserInRole },
            new SecurityRelation { ParentId = 2, ChildId = 2, InclusionType = InclusionType.UserInRole }
        };
    }
}
