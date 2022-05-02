using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar uma nova postagem</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewPostDTO
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required, StringLength(50)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [Required, StringLength(50)]
        public string EmailCreator { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public NewPostDTO(string title, string description, string photograph, string emailCreator, string themeDescription)
        {
            Title = title;
            Description = description;
            Photograph = photograph;
            EmailCreator = emailCreator;
            ThemeDescription = themeDescription;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para atualizar uma postagem</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpDatePostDTO
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required, StringLength(50)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public UpDatePostDTO(string title, string description, string photograph, string themeDescription)
        {
            Title = title;
            Description = description;
            Photograph = photograph;
            ThemeDescription = themeDescription;
        }
    }
}
