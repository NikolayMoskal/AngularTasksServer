using MediaItemsServer.Helpers;
using MediaItemsServer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaItemsServer.Controllers
{
    [Route(Consts.RolesRoute)]
    [Authorize(Roles = Consts.User)]
    public class RoleController : CustomController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return Json(_roleService.GetAll());
        }
    }
}
