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
    public class UserRepositoryTest 
    {
        private PersonalBlogContext _context;
        private IUser _repository;

        [TestMethod]
        public async Task CreateFourUsersInBankReturnFourUsers()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco
            await _repository.AddUserAsync(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            await _repository.AddUserAsync(
                new NewUserDTO("Mallu Boaz", "mallu@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            await _repository.AddUserAsync(
                new NewUserDTO("Catarina Boaz", "catarina@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            await _repository.AddUserAsync(
                new NewUserDTO("Pamela Boaz", "pamela@email.com", "134652", "URLFOTO",TypeUser.Commom)
            );

            //WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }

        [TestMethod]
        public async Task GetUserByEmailReturnNotNull()

        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
                new NewUserDTO("Zenildo Boaz", "zenildo@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = await _repository.GetUserByEmailAsync("zenildo@email.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task GetUserByIdReturnNotNullAndNameUser()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
                new NewUserDTO("Neusa Boaz", "neusa@email.com", "134652", "URLFOTO" ,TypeUser.Commom)
            );

            //WHEN - Quando pesquiso pelo id 1
            var user = await _repository.GetUserByIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }

        [TestMethod]
        public async Task UpdateUserReturnUserUpdated()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
                new NewUserDTO("Estefânia Boaz", "estefania@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            //WHEN - Quando atualizamos o usuario
            await _repository.AttUserAsync(
                new UpdateUserDTO(1, "Estefânia Moura", "123456", "URLFOTONOVA")
            );

            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            var antigo = await _repository.GetUserByEmailAsync("estefania@email.com");

            Assert.AreEqual(
                "Estefânia Moura",
                _context.Users.FirstOrDefault(u => u.Id == antigo.Id).Name
            );

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
                "123456",
                _context.Users.FirstOrDefault(u => u.Id == antigo.Id).Password
            );
        }

    }
}