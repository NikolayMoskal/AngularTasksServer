using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;
using Newtonsoft.Json;

namespace MediaItemsServer.Services
{
    public class MediaItemsService : IMediaItemsService
    {

        public IList<MediaItem> GetAll()
        {
            using var fileStream = new StreamReader("media-items.json");
            var content = fileStream.ReadToEnd();
            
            return JsonConvert.DeserializeObject<List<MediaItem>>(content) ?? new List<MediaItem>();
        }

        public MediaItem GetById(string id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void SaveOrUpdate(MediaItem item)
        {
            var items = GetAll().ToList();
            items.RemoveAll(x => x.Id == item.Id);
            items.Add(item);

            using var streamWriter = new StreamWriter("media-items.json", new FileStreamOptions
            {
                Access = FileAccess.Write,
                Mode = FileMode.Truncate,
                Share = FileShare.Read
            });
            var content = JsonConvert.SerializeObject(items);
            streamWriter.Write(content);
        }

        public void Delete(string id)
        {
            var items = GetAll().Where(x => x.Id != id);

            using var streamWriter = new StreamWriter("media-items.json", new FileStreamOptions
            {
                Access = FileAccess.Write,
                Mode = FileMode.Truncate,
                Share = FileShare.Read
            });
            var content = JsonConvert.SerializeObject(items);
            streamWriter.Write(content);
        }
    }
}
