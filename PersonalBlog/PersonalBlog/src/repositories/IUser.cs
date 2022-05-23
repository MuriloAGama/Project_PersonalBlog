using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task AddUserAsync(NewUserDTO newUser);
        Task AttUserAsync(UpdateUserDTO UpdateUser);
        Task DeleteUserAsync(int id);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<List<UserModel>> GetUserByNameAsync(string name);
    }
}
