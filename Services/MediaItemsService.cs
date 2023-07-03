using MediaItemsServer.Data.Contracts;
using MediaItemsServer.Models;
using MediaItemsServer.Services.Contracts;

namespace MediaItemsServer.Services
{
    public class MediaItemsService : IMediaItemsService
    {
        private readonly IMediaItemsRepository _mediaItemsRepository;

        public MediaItemsService(IMediaItemsRepository mediaItemsRepository)
        {
            _mediaItemsRepository = mediaItemsRepository;
        }

        public IList<MediaItem> GetAll()
        {
            return _mediaItemsRepository.GetAll();
        }

        public MediaItem GetById(string id)
        {
            return _mediaItemsRepository.GetById(id);
        }

        public void SaveOrUpdate(MediaItem item)
        {
            _mediaItemsRepository.SaveOrUpdate(item);
        }

        public void Delete(string id)
        {
            _mediaItemsRepository.Delete(id);
        }
    }
}
