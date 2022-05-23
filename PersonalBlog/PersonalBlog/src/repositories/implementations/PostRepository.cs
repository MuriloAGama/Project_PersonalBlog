using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.src.repositories.implementations
{
    /// <summary>
    /// <para>Resumo: Class responsible for implementing IPost</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>

    public class PostRepository : IPost
    {

        #region Attributes

        private readonly PersonalBlogContext _context;

        #endregion Attributes

        #region Constructor
        public PostRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion Constructor

        #region Methods

        /// <summary>
        /// <para>Resumo: Asynchronous method to update a post</para>
        /// </summary>
        /// <param name="post">UpdatePostDTO</param>

        public async Task AttPostAsync(UpDatePostDTO post)
        {
            var existingPost = await GetPostByIdAsync(post.Id);
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Photograph = post.Photograph;
            existingPost.Theme = _context.Themes.FirstOrDefault(
            t => t.Description == post.ThemeDescription);

            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to delete a post</para>
        /// </summary>
        /// <param name="id">Id of post</param>
        /// 
        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to get all posts</para>
        /// </summary>
        /// <return>List PostModel></return>
        public async Task<List<PostModel>> GetAllPostsAsync()
        {
            return await _context.Posts
           .Include(p => p.Creator)
           .Include(p => p.Theme)
           .ToListAsync();

        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to get a post by Id</para>
        /// </summary>
        /// <param name="id">Id of post</param>
        /// <return>PostModel</return>
        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts
            .Include(p => p.Creator)
            .Include(p => p.Theme)
            .FirstOrDefaultAsync(p => p.Id == id);

        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to get fetch posts by search</para>
        /// </summary>
        /// <param name="title">Post title </param>
        /// <param name="themeDescription">Theme Description</param>
        /// <param name="nameCreator">Creator name</param>
        /// <return>List PostModel</return>
        public async Task<List<PostModel>> GetPostsBySearchAsync(string title, string themeDescription, string nameCreator)
        {
            switch (title, themeDescription, nameCreator)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();
                case (null, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();
                case (null, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription))
                    .ToListAsync();
                case (_, null, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToListAsync();
                case (_, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(themeDescription))
                    .ToListAsync();
                case (null, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(themeDescription) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();
                case (_, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();
                case (_, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(themeDescription) |
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();
            }
        }


        /// <summary>
        /// <para>Resumo: Asynchronous method to save a new post</para>
        /// </summary>
        /// <param name="post">NewPostDTO</param>
        public async Task NewPostAsync(NewPostDTO post)
        {
            await _context.Posts.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photograph = post.Photograph,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.EmailCreator),
                Theme = _context.Themes.FirstOrDefault(t => t.Description == post.ThemeDescription)
            });
            await _context.SaveChangesAsync();
        }

        #endregion Methods
    }
}
