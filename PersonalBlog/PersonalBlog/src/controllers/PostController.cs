using Microsoft.AspNetCore.Mvc;
using PersonalBlog.src.dtos;
using PersonalBlog.src.repositories;

namespace PersonalBlog.src.controllers
{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos

        private readonly IPost _repository;

        #endregion


        #region Construtores

        public PostagemControlador(IPost repository)
        {
            _repository = repository;
        }

        #endregion


        #region Métodos

        [HttpGet("id/{idPost}")]
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            var post = _repository.GetPostById(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var list = _repository.GetAllPosts();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("search")]
        public IActionResult GetPostsBySearch(
            [FromQuery] string title,
            [FromQuery] string themeDescription,
            [FromQuery] string nameCreator)
        {
            var posts = _repository.GetPostsBySearch(title, themeDescription, nameCreator);

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        [HttpPost]
        public IActionResult NewPost([FromBody] NewPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewPost(post);

            return Created($"api/Posts", post);
        }

        [HttpPut]
        public IActionResult AttPost([FromBody] UpDatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AttPost(post);

            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }

        #endregion
    }
}