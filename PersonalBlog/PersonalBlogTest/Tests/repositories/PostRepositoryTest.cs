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
        public async Task CreateThreePostInSystemReturnThree()
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
            await _repositoryU.AddUserAsync(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            await _repositoryU.AddUserAsync(
                new NewUserDTO("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            // AND - E que registro 2 temas
            await _repositoryT.AddThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.AddThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco todas as postagens
            
            var posts = await _repositoryP.GetAllPostsAsync();

            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, posts.Count());
        }

        [TestMethod]
        public async Task UpdatePostReturnPostUpdate()
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
            await _repositoryU.AddUserAsync(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            // AND - E que registro 1 tema
            await _repositoryT.AddThemeAsync(new NewThemeDTO("COBOL"));
            await _repositoryT.AddThemeAsync(new NewThemeDTO("C#"));

            // AND - E que registro 1 postagem
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            await _repositoryP.AttPostAsync(
                new UpDatePostDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            var post = await _repositoryP.GetPostByIdAsync(1);

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual("C# é muito massa", post.Title);
            Assert.AreEqual("C# é muito utilizada no mundo", post.Description);
            Assert.AreEqual("URLDAFOTOATUALIZADA", post.Photograph);
            Assert.AreEqual("C#", post.Description);
        }

        [TestMethod]
        public async Task GetPostBySearchReturnCustom()
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
            await _repositoryU.AddUserAsync(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            await _repositoryU.AddUserAsync(
                new NewUserDTO("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO", TypeUser.Commom)
            );

            // AND - E que registro 2 temas
            await _repositoryT.AddThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.AddThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            var postTest1 = await _repositoryP.GetPostsBySearchAsync("massa", null, null);
            var postTest2 = await _repositoryP.GetPostsBySearchAsync(null, "C#", null);
            var postTest3 = await _repositoryP.GetPostsBySearchAsync(null, null, "gustavo@email.com");

            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(2, postTest1.Count);
            Assert.AreEqual(2, postTest1.Count);
            Assert.AreEqual(2, postTest1.Count);
        }
    }
}