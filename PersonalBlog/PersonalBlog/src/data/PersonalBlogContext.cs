using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.models;

namespace PersonalBlog.src.data
{
    public class PersonalBlogContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ThemeModel> Themes { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public PersonalBlogContext(DbContextOptions<PersonalBlogContext> options) : base(options)
        {

        }
    }
}
