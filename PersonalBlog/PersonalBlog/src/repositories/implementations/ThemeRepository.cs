using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;

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
        public void AddTheme(NewThemeDTO newTheme)
        {
            _context.Themes.Add(new ThemeModel
            {
                Description = newTheme.Description

            });

            _context.SaveChanges();
        }

        public void AttTheme(UpdateThemeDTO newTheme)
        {
            var existingTheme = GetThemeById(newTheme.Id);
            existingTheme.Description = newTheme.Description;
            _context.Themes.Update(existingTheme);
            _context.SaveChanges();

        }

        public void DeleteTheme(int id)
        {
            _context.Themes.Remove(GetThemeById(id));
            _context.SaveChanges();

        }

        public List<ThemeModel> GetAllThemes()
        {
            return _context.Themes
                    .ToList();
        }

        public List<ThemeModel> GetThemeByDescription(string description)
        {
            return _context.Themes
            .Where(t => t.Description.Contains(description))
            .ToList();
        }

        public ThemeModel GetThemeById(int id)
        {
            return _context.Themes.FirstOrDefault(p => p.Id == id);

        }
    }

    #endregion Method
}

