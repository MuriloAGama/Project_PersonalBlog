using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;

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
        void NewPost(NewPostDTO post);
        void AttPost(UpDatePostDTO post);
        void DeletePost(int id);
        PostModel GetPostById(int id);
        List<PostModel> GetAllPosts();
        List<PostModel> GetPostsBySearch(string title, string themeDescription, string nameCreator);

    }
}
