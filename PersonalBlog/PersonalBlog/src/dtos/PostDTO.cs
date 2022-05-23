using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.src.dtos
{
    /// <summary>
    /// <para>Resumo: Mirror class to create a new post</para>
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
    /// <para>Resumo: Mirror class to update a new post</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpDatePostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required, StringLength(50)]
        public string Description { get; set; }

        public string Photograph { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public UpDatePostDTO(int id, string title, string description, string photograph, string themeDescription)
        {
            Id = id;
            Title = title;
            Description = description;
            Photograph = photograph;
            ThemeDescription = themeDescription;
        }
    }
}
