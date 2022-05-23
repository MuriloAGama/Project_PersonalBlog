using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.models;

namespace PersonalBlog.src.data
{
    /// <summary>
    /// <para>Resumo: Class context, responsible for loading context and setting DbSets</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
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
