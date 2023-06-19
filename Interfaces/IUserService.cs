using MediaItemsServer.Models;

namespace MediaItemsServer.Interfaces
{
    public interface IUserService
    {
        User Get(string userName);
        void SaveOrUpdate(User user);
        void Delete(string userName);
    }
}
