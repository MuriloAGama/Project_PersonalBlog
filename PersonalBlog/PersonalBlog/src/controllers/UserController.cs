using Microsoft.AspNetCore.Mvc;
using PersonalBlog.src.dtos;
using PersonalBlog.src.repositories;

namespace PersonalBlog.src.controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos

        private readonly IUser _repository;

        #endregion


        #region Construtores

        public UsuarioControlador(IUser repository)
        {
            _repository = repository;
        }

        #endregion


        #region Métodos

        [HttpGet("id/{idUser}")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUserByName([FromQuery] string userName)
        {
            var users = _repository.GetUserByName(userName);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AddUser(user);
            return Created($"api/Users/email/{user.Email}", user);
        }

        [HttpPut]
        public IActionResult AttUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AttUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }

        #endregion

    }
}
