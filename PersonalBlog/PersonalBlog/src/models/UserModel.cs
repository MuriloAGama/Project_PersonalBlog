using PersonalBlog.src.utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PersonalBlog.src.models
{
    /// <summary>
    /// <para>Resumo: Class responsible for representing tb_users in the database.</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    
    [Table("tb_users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photograph { get; set; }

        [Required]
        public TypeUser Type { get; set; }


        [JsonIgnore, InverseProperty("Creator")]
        public List<PostModel> MyPosts { get; set; }
    }
}
