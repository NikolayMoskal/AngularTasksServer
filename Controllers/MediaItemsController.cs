using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaItemsServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MediaItemsController : ControllerBase
    {
        private readonly IMediaItemsService _mediaItemsService;

        public MediaItemsController(IMediaItemsService mediaItemsService)
        {
            _mediaItemsService = mediaItemsService;
        }

        [HttpGet]
        [Authorize]
        public IList<MediaItem> GetAll()
        {
            return _mediaItemsService.GetAll();
        }

        [HttpGet("{id}")]
        public MediaItem GetById(string id)
        {
            return _mediaItemsService.GetById(id);
        }

        [HttpPost]
        public void SaveOrUpdate(MediaItem item)
        {
            _mediaItemsService.SaveOrUpdate(item);
        }

        [HttpDelete]
        public void Delete(string id)
        {
            _mediaItemsService.Delete(id);
        }
    }
}
