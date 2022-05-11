using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;

namespace PersonalBlog.src.repositories.implementations
{

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

        public void AttPost(UpDatePostDTO post)
        {
            var existingPost = GetPostById(post.Id);
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Photograph = post.Photograph;
            existingPost.Theme = _context.Themes.FirstOrDefault(
            t => t.Description == post.ThemeDescription);

            _context.Posts.Update(existingPost);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostById(id));
            _context.SaveChanges();

        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Posts.ToList();
            return _context.Posts
           .Include(p => p.Creator)
           .Include(p => p.Theme)
           .ToList();

        }

        public PostModel GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);

        }

        public List<PostModel> GetPostsBySearch(string title, string themeDescription, string nameCreator)
        {
            switch (title, themeDescription, nameCreator)
            {
                case (null, null, null):
                    return GetAllPosts();
                case (null, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (null, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription))
                    .ToList();
                case (_, null, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToList();
                case (_, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(themeDescription))
                    .ToList();
                case (null, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(themeDescription) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (_, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (_, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(themeDescription) |
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
            }
        }



        public void NewPost(NewPostDTO post)
        {
            _context.Posts.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photograph = post.Photograph,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.EmailCreator),
                Theme = _context.Themes.FirstOrDefault(t => t.Description == post.ThemeDescription)
            });
            _context.SaveChanges();
        }

        #endregion Methods
    }
}
