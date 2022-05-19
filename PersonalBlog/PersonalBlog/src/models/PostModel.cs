using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_postagens no banco.</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    
    [Table("tb_posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required, StringLength(50)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [ForeignKey("fk_user")]
 
        public UserModel Creator { get; set; }

        [ForeignKey("fk_theme")]
        public ThemeModel Theme { get; set; }
    }
}