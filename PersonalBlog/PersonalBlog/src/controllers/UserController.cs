using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using PersonalBlog.src.repositories;
using PersonalBlog.src.services;
using System;
using System.Threading.Tasks;

namespace PersonalBlog.src.controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attribute

        private readonly IUser _repository;

        private readonly IAuthentication _services;

        #endregion


        #region Constructor

        public UserController(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return the user</response>
        /// <response code="404">User not existent</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "Common,Administrator")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Get user by Name
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return the user</response>
        /// <response code="204">Name does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "Common,Administrator")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string userName)
        {
            var users = await _repository.GetUserByNameAsync(userName);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        /// <summary>
        /// Get user by Email
        /// </summary>
        /// <param name="emailUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return the user</response>
        /// <response code="404">Email não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "Common,Administrator")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Gustavo Boaz",
        ///        "email": "gustavo@domain.com",
        ///        "password": "134652",
        ///        "photo": "URLFOTO",
        ///        "type": "Common"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return user created</response>
        /// <response code="400">Request error</response>
        /// <response code="401">E-mail already registered</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUserAsync([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _services.CreateUserNotDuplicateAsync(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        /// <summary>
        /// updated user
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     PUT /api/Users
        ///     {
        ///        "id": 1,    
        ///        "name": "Gustavo Boaz",
        ///        "password": "134652",
        ///        "photo": "URLFOTO"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return user update</response>
        /// <response code="400">Error in the request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "Common,Administrator")]
        public async Task<ActionResult> AttUserAsync([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            await _repository.AttUserAsync(user);
            return Ok(user);
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">User deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            await _repository.DeleteUserAsync(idUser);
            return NoContent();
        }

        #endregion

    }
}
