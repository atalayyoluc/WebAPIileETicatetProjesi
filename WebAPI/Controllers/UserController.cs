using Busines.Abstract;
using Entities.DTOS.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result=await userService.GetListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> GetList(int id)
        {
            var result = await userService.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add([FromBody] UserAddDTO userAddDTO)
        {
            var result=await userService.AddAsync(userAddDTO);
            if (result != null)
            {
            return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdate)
        {
            var result = await userService.UpdateAsync(userUpdate);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            var result=await userService.DeleteAsync(id);
            if (result)
            {
                return Ok(true);
            }
            return BadRequest(false);
        }

    }
}
