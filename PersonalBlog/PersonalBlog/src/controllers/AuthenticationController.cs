using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.src.dtos;
using PersonalBlog.src.services;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Autenticacao")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Atributos

        private readonly IAuthentication _services;

        #endregion


        #region Construtores

        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Pegar Autorização
        /// </summary>
        /// <param name="authentication">AuthenticationDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Autenticacao
        ///     {
        ///        "email": "gustavo@domain.com",
        ///        "senha": "134652"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ou senha invalido</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthenticationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AuthenticationAsync([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var authorization = await _services.GetAuthorizationAsync(authentication);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        #endregion
    }
}