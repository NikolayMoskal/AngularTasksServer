using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MediaItemsServer.Controllers
{
    [ApiController]
    public class CustomController : ControllerBase
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

        protected IActionResult Json(object value)
        {
            return new JsonResult(value, _serializerSettings);
        }
    }
}
