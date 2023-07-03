using MediaItemsServer.Models;

namespace MediaItemsServer.Services.Contracts
{
    public interface IMediaItemsService
    {
        IList<MediaItem> GetAll();
        MediaItem GetById(string id);
        void SaveOrUpdate(MediaItem item);
        void Delete(string id);
    }
}
