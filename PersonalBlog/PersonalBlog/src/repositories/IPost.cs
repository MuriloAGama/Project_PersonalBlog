using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalBlog.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de postagem</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPost
    {
        Task NewPostAsync(NewPostDTO post);
        Task AttPostAsync(UpDatePostDTO post);
        Task DeletePostAsync(int id);
        Task<PostModel> GetPostByIdAsync(int id);
        Task<List<PostModel>> GetAllPostsAsync();
        Task<List<PostModel>> GetPostsBySearchAsync(string title, string themeDescription, string nameCreator);

    }
}
