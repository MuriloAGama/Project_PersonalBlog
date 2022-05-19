using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalBlog.src.data;
using PersonalBlog.src.models;
using System.Linq;

namespace PersonalBlogTest.Tests.data
{
    [TestClass]
    public class PersonalBlogContextTest
    {
        private PersonalBlogContext _context;

        [TestInitialize]
        public void Inicio()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_personalblog")
                .Options;

            _context = new PersonalBlogContext(opt);
        }

        [TestMethod]
        public void InsertNewUserInTheBankReturnUser()
        {
            UserModel user = new UserModel();

            user.Name = "Karol Boaz";
            user.Email = "karol@email.com";
            user.Password = "134652";
            user.Photograph = "AKITAOLINKDAFOTO";

            _context.Users.Add(user); // Adcionando usuario

            _context.SaveChanges(); // Commita criação

            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Email == "karol@email.com"));
        }
    }
}