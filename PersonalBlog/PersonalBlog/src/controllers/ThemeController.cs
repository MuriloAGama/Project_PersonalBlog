using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using PersonalBlog.src.repositories;
using System.Threading.Tasks;

namespace PersonalBlog.src.controllers
{
    [ApiController]
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Attribute

        private readonly ITheme _repository;

        #endregion


        #region Constructor

        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Get all themes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">List of themes </response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repository.GetAllThemesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Pegar tema pelo Id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return the theme</response>
        /// <response code="404">Theme does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        /// <summary>
        /// Get theme by Description
        /// </summary>
        /// <param name="descriptionTheme">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return themes</response>
        /// <response code="204">Description does not exist existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string descriptionTheme)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        /// <summary>
        /// Create new Theme
        /// </summary>
        /// <param name="theme">NewThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// request example:
        ///
        ///     POST /api/Themes
        ///     {
        ///        "description": "CSHARP",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return theme created</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task <ActionResult> AddThemeAsync([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AddThemeAsync(theme);

            return Created($"api/Themes", theme);
        }

        /// <summary>
        /// Update Theme
        /// </summary>
        /// <param name="theme">UpdateThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// request example:
        ///
        ///     PUT /api/Themes
        ///     {
        ///        "id": 1,    
        ///        "description": "CSHARP"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return theme updated</response>
        /// <response code="400">Error in the request</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AttThemeAsync([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AttThemeAsync(theme);

            return Ok(theme);
        }


        /// <summary>
        /// Dele theme by Id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Theme deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            await _repository.DeleteThemeAsync(idTheme);
            return NoContent();
        }

        #endregion
    }
}