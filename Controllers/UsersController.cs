using Microsoft.AspNetCore.Mvc;
using UserManagamentApi_1.Services;
using UserManagamentApi_1.DTOs;

namespace UserManagamentApi_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }


        // REGISTER

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                var user = await _service.Register(dto);

                return Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // LOGIN

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            try
            {
                var user = await _service.Login(dto);

                return Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET ALL

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAll();

            // No devolvemos hash ni salt
            var result = users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Email
            });

            return Ok(result);
        }


        // DELETE

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound("Usuario no encontrado");

            return Ok("Usuario eliminado");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _service.Update(id, dto);

                if (user == null)
                    return NotFound("Usuario no encontrado");

                return Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}