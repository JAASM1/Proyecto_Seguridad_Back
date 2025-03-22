using back_sistema_de_eventos.Models.DTOs;
using back_sistema_de_eventos.Services.IService.IUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace back_sistema_de_eventos.Controllers.Auth
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        // UserController
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Obtener los usuarios
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _userService.GetUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = "Error en el servidor", error = ex.Message});
            }
        }

        //Crear el usuario
        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new { message = "Los datos del usuario son invalidos", errors = ModelState.Values.SelectMany(v => v.Errors) });
        //    }
        //    try
        //    {
        //        bool result = await _userService.CreateUser(request);

        //        if (result)
        //        {
        //            return CreatedAtAction(nameof(GetUsers), request);
        //        }
        //        return StatusCode(500, new { message = "Hubo un problema al crear el usuario" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
        //    }
        //}


        //Obtener usuario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult>GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if(user == null)
            {
                return NotFound(new { sucess = false, message = "Usuario no encontrado" });
            }
            return Ok(new {succes = true, message = "Usuario obtenido correctamente", data=user});
        }


        // Editar usuario
        [HttpPut("Editar/{Id}")]
        public async Task<IActionResult> EditUser(int Id, [FromBody] UserEditDTO request)
        {
            try
            {
                bool update = await _userService.EditUser(Id, request);
                if (!update)
                {
                    return BadRequest(new { message = "No se puede actualizar el usuario" });
                }
                return Ok(new { message = "Usuario actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }

        // Eliminar usuario
        [HttpDelete("Eliminar/{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            try
            {
                bool result = await _userService.DeleteUser(Id);
                if (result)
                {
                    return Ok(new { success = true, message = "Usuario eliminado exitosamente" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Error al eliminar el usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }
    }
}
