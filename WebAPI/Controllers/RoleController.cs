using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody]string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);
            if(result.Success)
                return Ok(result);  
            return BadRequest(result);
        }
    }
}
