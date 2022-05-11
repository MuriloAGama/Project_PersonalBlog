using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;

namespace PersonalBlog.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de tema</para>
    /// <para>Criado por: Murilo Gama</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface ITheme
    {
            void AddTheme(NewThemeDTO newTheme);
            void AttTheme(UpdateThemeDTO newTheme);
            void DeleteTheme(int id);

            ThemeModel GetThemeById(int id);
            List<ThemeModel> GetThemeByDescription(string description);
            List<ThemeModel> GetAllThemes();



    }
}


