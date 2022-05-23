using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PersonalBlog.src.models
{
    /// <summary>
    /// <para>Resumo: Class responsible for representing tb_themes in the database.</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    
    [Table("tb_themes")]
    public class ThemeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Description { get; set; }

        [JsonIgnore]
        public List<PostModel> PostRelated { get; set; }
    }
}
    

