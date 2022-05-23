using PersonalBlog.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.src.dtos
{
    /// <summary>
    /// <para>Resumo: Mirror class to create a new user</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewUserDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photograph { get; set; }

        [Required]
        public TypeUser Type { get; set; }


        public NewUserDTO(string name, string email, string password, string photograph, TypeUser type)
        {
            Name = name;
            Email = email;
            Password = password;
            Photograph = photograph;
            Type = type;
        }
    }

    /// <summary>
    /// <para>Resumo: Mirror class to update a user </para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateUserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photograph { get; set; }

 

        public UpdateUserDTO(int id, string name, string password, string photograph)
        {
            Id = id;
            Name = name;
            Password = password;
            Photograph = photograph;
           
        }
    }
}
