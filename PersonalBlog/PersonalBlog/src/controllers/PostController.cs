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
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Attribute

        private readonly IPost _repository;

        #endregion


        #region Constructor

        public PostController(IPost repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Get post by Id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return post</response>
        /// <response code="404">Post does not exist</response>
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        /// <summary>
        /// Get all post
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Post list</response>
        /// <response code="204">Empty list</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }


        /// <summary>
        /// Get post by search
        /// </summary>
        /// <param name="title">string</param>
        /// <param name="themeDescription">string</param>
        /// <param name="nameCreator">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return post</response>
        /// <response code="204">Does not exist for this search</response>
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetPostsBySearchAsync(
            [FromQuery] string title,
            [FromQuery] string themeDescription,
            [FromQuery] string nameCreator)
        {
            var posts = await _repository.GetPostsBySearchAsync(title, themeDescription, nameCreator);

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        /// <summary>
        /// Create new post
        /// </summary>
        /// <param name="post">NewPostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Posts
        ///     {  
        ///        "title": "Dotnet Core mudando o mundo", 
        ///        "description": "Uma ferramenta muito boa para desenvolver aplicações web",
        ///        "photo": "URLDAIMAGEM",
        ///        "emailCreator": "gustavo@domain.com",
        ///        "ThemeDescription": "CSHARP"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return post created</response>
        /// <response code="400">Error in the request</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewPostAsync([FromBody] NewPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(post);

            return Created($"api/Posts", post);
        }

        /// <summary>
        /// Update Theme
        /// </summary>
        /// <param name="post">UpdatePostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// request example:
        ///
        ///     PUT /api/Post
        ///     {
        ///        "id": 1,   
        ///        "title": "Dotnet Core mudando o mundo", 
        ///        "description": "Uma ferramenta muito boa para desenvolver aplicações web",
        ///        "phoyo": "URLDAIMAGEM",
        ///        "ThemeDescription": "CSHARP"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return post update</response>
        /// <response code="400">Error in the request</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AttPostAsync([FromBody] UpDatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AttPostAsync(post);

            return Ok(post);
        }

        /// <summary>
        /// Delete post by Id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Post delete</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }

        #endregion
    }
}