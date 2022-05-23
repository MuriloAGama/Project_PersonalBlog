using PersonalBlog.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.src.dtos
{
    /// <summary>
    /// <para>Resumo: Mirror class to authenticate a user</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class AuthenticationDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    /// <summary>
    /// <para>Resumo: Mirror class to represent self</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TypeUser Type { get; set; }
        public string Token { get; set; }

        public AuthorizationDTO(int id, string email, TypeUser type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}
