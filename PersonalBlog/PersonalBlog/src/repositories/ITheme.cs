using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            Task AddThemeAsync(NewThemeDTO newTheme);
            Task AttThemeAsync(UpdateThemeDTO theme);
            Task DeleteThemeAsync(int id);
            Task<ThemeModel> GetThemeByIdAsync(int id);
            Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description);
            Task<List<ThemeModel>> GetAllThemesAsync();



    }
}


