using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.repositories;
using PersonalBlog.src.repositories.implementations;
using PersonalBlog.src.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlogTest.Tests.repositories
{
    [TestClass]
    public class PostRepositoryTest
    {
        private PersonalBlogContext _context;
        private IUser _repositoryU;
        private ITheme _repositoryT;
        private IPost _repositoryP;

        [TestMethod]
        public void CreateThreePostInSystemReturnThree()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                 .UseInMemoryDatabase(databaseName: "db_blogpessoal21")
                 .Options;

            _context = new PersonalBlogContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            _repositoryU.AddUser(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            _repositoryU.AddUser(
                new NewUserDTO("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            // AND - E que registro 2 temas
            _repositoryT.AddTheme(new NewThemeDTO("C#"));
            _repositoryT.AddTheme(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco todas as postagens
            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, _repositoryP.GetAllPosts().Count());
        }

        [TestMethod]
        public void UpdatePostReturnPostUpdate()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 1 usuarios
            _repositoryU.AddUser(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            // AND - E que registro 1 tema
            _repositoryT.AddTheme(new NewThemeDTO("COBOL"));
            _repositoryT.AddTheme(new NewThemeDTO("C#"));

            // AND - E que registro 1 postagem
            _repositoryP.NewPost(
                new NewPostDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            _repositoryP.AttPost(
                new UpDatePostDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual("C# é muito massa", _repositoryP.GetPostById(1).Title);
            Assert.AreEqual("C# é muito utilizada no mundo", _repositoryP.GetPostById(1).Description);
            Assert.AreEqual("URLDAFOTOATUALIZADA", _repositoryP.GetPostById(1).Photograph);
            Assert.AreEqual("C#", _repositoryP.GetPostById(1).Theme.Description);
        }

        [TestMethod]
        public void GetPostBySearchReturnCustom()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            _repositoryU.AddUser(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            _repositoryU.AddUser(
                new NewUserDTO("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            // AND - E que registro 2 temas
            _repositoryT.AddTheme(new NewThemeDTO("C#"));
            _repositoryT.AddTheme(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(2, _repositoryP.GetPostsBySearch("massa", null, null).Count);
            Assert.AreEqual(2, _repositoryP.GetPostsBySearch(null, "C#", null).Count);
            Assert.AreEqual(2, _repositoryP.GetPostsBySearch(null, null, "Gustavo Boaz").Count);
        }
    }
}