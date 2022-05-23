using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.src.repositories.implementations
{
    /// <summary>
    /// <para>Resumo: Class responsible for implementing ITema</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class ThemeRepository : ITheme
    {

        #region Attributes

        private readonly PersonalBlogContext _context;

        #endregion Attributes

        #region Constructor
        public ThemeRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// <para>Resumo: Asynchronous method to save a new theme</para>
        /// </summary>
        /// <param name="newTheme">NovoTemaDTO</param>
        public async Task AddThemeAsync(NewThemeDTO newTheme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = newTheme.Description

            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to update a theme</para>
        /// </summary>
        /// <param name="Theme">UpdateThemeDTO</param>
        public async Task AttThemeAsync(UpdateThemeDTO Theme)
        {
            var existingTheme = await GetThemeByIdAsync(Theme.Id);
            existingTheme.Description = Theme.Description;
            _context.Themes.Update(existingTheme);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to delete a theme</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to get all themes</para>
        /// </summary>
        /// <return>Lista TemaModelo</return>
        public async Task<List<ThemeModel>> GetAllThemesAsync()
        {
            return await _context.Themes
                    .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Asynchronous method to get themes by description</para>
        /// </summary>
        /// <param name="description">Descrição do tema</param>
        /// <return>Lista TemaModelo</return>
        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
            .Where(t => t.Description.Contains(description))
            .ToListAsync();
        }

        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(p => p.Id == id);

        }

        #endregion Method
    }

}

