using PersonalBlog.src.dtos;
using PersonalBlog.src.models;

namespace PersonalBlog.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de usuario</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>

    public interface IUser
    {
        void AddUser(NewUserDTO newUsuario);
        void AttUser(UpdateUserDTO newUsuario);
        void DeleteUser(int id);

        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
        UserModel GetUserByName(string name);
    }
}
