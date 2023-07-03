using MediaItemsServer.Helpers;
using MediaItemsServer.Models;
using MediaItemsServer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaItemsServer.Controllers
{
    [Route("api/media/[action]")]
    [Authorize(Roles = Consts.User)]
    public class MediaItemsController : CustomController
    {
        private readonly IMediaItemsService _mediaItemsService;

        public MediaItemsController(IMediaItemsService mediaItemsService)
        {
            _mediaItemsService = mediaItemsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IList<MediaItem> GetAll()
        {
            return _mediaItemsService.GetAll();
        }

        [HttpGet]
        public MediaItem Get([FromQuery] string id)
        {
            return _mediaItemsService.GetById(id);
        }

        [HttpPost]
        [Authorize(Roles = Consts.Administrator)]
        public void SaveOrUpdate(MediaItem item)
        {
            _mediaItemsService.SaveOrUpdate(item);
        }

        [HttpDelete]
        [Authorize(Roles = Consts.Administrator)]
        public void Delete(string id)
        {
            _mediaItemsService.Delete(id);
        }
    }
}
