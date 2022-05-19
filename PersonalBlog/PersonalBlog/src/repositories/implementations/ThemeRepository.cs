using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.src.repositories.implementations
{
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

        #region Method
        public async Task AddThemeAsync(NewThemeDTO newTheme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = newTheme.Description

            });

            await _context.SaveChangesAsync();
        }

        public async Task AttThemeAsync(UpdateThemeDTO newTheme)
        {
            var existingTheme = await GetThemeByIdAsync(newTheme.Id);
            existingTheme.Description = newTheme.Description;
            _context.Themes.Update(existingTheme);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();

        }

        public async Task<List<ThemeModel>> GetAllThemesAsync()
        {
            return await _context.Themes
                    .ToListAsync();
        }

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
    }

    #endregion Method
}

