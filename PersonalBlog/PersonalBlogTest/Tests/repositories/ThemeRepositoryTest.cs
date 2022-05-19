using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.repositories;
using PersonalBlog.src.repositories.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlogTest.Tests.repositories
{
    [TestClass]
    public class ThemeRepositoryText
    {
        private PersonalBlogContext _context;
        private ITheme _repository;

        [TestMethod]
        public async Task CreateFourThemesInBankReturnFourThemes2()   
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco
            await _repository.AddThemeAsync(new NewThemeDTO("C#"));
            await _repository.AddThemeAsync(new NewThemeDTO("Java"));
            await _repository.AddThemeAsync(new NewThemeDTO("Python"));
            await _repository.AddThemeAsync(new NewThemeDTO("JavaScript"));

            //THEN - Entao deve retornar 4 temas
            var themes =  await _repository.GetAllThemesAsync();

            Assert.AreEqual(4, themes.Count);
        }

        [TestMethod]
        public async Task GetThemeByIdReturnTheme1() 
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal11")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro C# no banco
            await _repository.AddThemeAsync(new NewThemeDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var tema = await _repository.GetThemeByIdAsync(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", tema.Description);
        }

        [TestMethod]
        public async Task GetThemeByDescriptionReturnThemes()
            
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal12")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Java no banco
            await _repository.AddThemeAsync(new NewThemeDTO("Java"));
            //AND - E que registro JavaScript no banco
            await _repository.AddThemeAsync(new NewThemeDTO("JavaScript"));

            //WHEN - Quando que pesquiso pela descricao Java
            var temas = await _repository.GetThemeByDescriptionAsync("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }

        [TestMethod]
        public async Task AlterThemePythonReturnThemeCobol()

        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal13")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Python no banco
            await _repository.AddThemeAsync(new NewThemeDTO("Python"));

            //WHEN - Quando passo o Id 1 e a descricao COBOL
            await _repository.AttThemeAsync(new UpdateThemeDTO(1, "COBOL"));

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL", await _repository.GetThemeByIdAsync(1));
        }

        [TestMethod]
        public async Task DeleteThemesReturnNull()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal14")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 temas no banco
            await _repository.AddThemeAsync(new NewThemeDTO("C#"));

            //WHEN - quando deleto o Id 1
            await _repository.DeleteThemeAsync(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(await _repository.GetThemeByIdAsync(1));
        }
    }
}