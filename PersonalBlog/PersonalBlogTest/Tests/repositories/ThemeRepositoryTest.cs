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
    public class TemaRepositorioTeste
    {
        private PersonalBlogContext _context;
        private ITheme _repository;

        [TestMethod]
        public void CreateFourThemesInBankReturnFourThemes2()   
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco
            _repository.AddTheme(new NewThemeDTO("C#"));
            _repository.AddTheme(new NewThemeDTO("Java"));
            _repository.AddTheme(new NewThemeDTO("Python"));
            _repository.AddTheme(new NewThemeDTO("JavaScript"));

            //THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, _repository.GetAllThemes().Count);
        }

        [TestMethod]
        public void GetThemeByIdReturnTheme1() 
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal11")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro C# no banco
            _repository.AddTheme(new NewThemeDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var tema = _repository.GetThemeById(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", tema.Description);
        }

        [TestMethod]
        public void GetThemeByDescriptionReturnThemes()
            
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal12")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Java no banco
            _repository.AddTheme(new NewThemeDTO("Java"));
            //AND - E que registro JavaScript no banco
            _repository.AddTheme(new NewThemeDTO("JavaScript"));

            //WHEN - Quando que pesquiso pela descricao Java
            var temas = _repository.GetThemeByDescription("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }

        [TestMethod]
        public void AlterThemePythonReturnThemeCobol()

        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal13")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Python no banco
            _repository.AddTheme(new NewThemeDTO("Python"));

            //WHEN - Quando passo o Id 1 e a descricao COBOL
            _repository.AttTheme(new UpdateThemeDTO(1, "COBOL"));

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL", _repository.GetThemeById(1).Description);
        }

        [TestMethod]
        public void DeleteThemesReturnNull()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal14")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 temas no banco
            _repository.AddTheme(new NewThemeDTO("C#"));

            //WHEN - quando deleto o Id 1
            _repository.DeleteTheme(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(_repository.GetThemeById(1));
        }
    }
}