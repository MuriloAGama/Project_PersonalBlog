using Microsoft.AspNetCore.Mvc;
using PersonalBlog.src.dtos;
using PersonalBlog.src.repositories;

namespace PersonalBlog.src.controllers
{
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Atributos

        private readonly ITheme _repository;

        #endregion


        #region Construtores

        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion


        #region Métodos

        [HttpGet]
        public IActionResult GetAllThemes()
        {
            var list = _repository.GetAllThemes();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("id/{idTheme}")]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeById(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        [HttpGet("Search")]
        public IActionResult PegarTemasPelaDescricao([FromQuery] string descriptionTheme)
        {
            var themes = _repository.GetThemeByDescription(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        [HttpPost]
        public IActionResult AddTheme([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AddTheme(theme);

            return Created($"api/Themes", theme);
        }

        [HttpPut]
        public IActionResult AttTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AttTheme(theme);

            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            return NoContent();
        }

        #endregion
    }
}