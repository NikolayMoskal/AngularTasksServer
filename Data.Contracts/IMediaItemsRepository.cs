using MediaItemsServer.Models;

namespace MediaItemsServer.Data.Contracts
{
    public interface IMediaItemsRepository
    {
        IList<MediaItem> GetAll();
        MediaItem GetById(string id);
        void SaveOrUpdate(MediaItem item);
        void Delete(string id);
    }
}
