﻿using Microsoft.EntityFrameworkCore;
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
    public class UsuarioRepositorioTeste
    {
        private PersonalBlogContext _context;
        private IUser _repository;

        [TestMethod]
        public void CreateFourUsersInBankReturnFourUsers()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco
            _repository.AddUser(
                new NewUserDTO("Gustavo Boaz", "gustavo@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            _repository.AddUser(
                new NewUserDTO("Mallu Boaz", "mallu@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            _repository.AddUser(
                new NewUserDTO("Catarina Boaz", "catarina@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            _repository.AddUser(
                new NewUserDTO("Pamela Boaz", "pamela@email.com", "134652", "URLFOTO",TypeUser.Commom)
            );

            //WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }

        [TestMethod]
        public void GetUserByEmailReturnNotNull()

        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUser(
                new NewUserDTO("Zenildo Boaz", "zenildo@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmail("zenildo@email.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetUserByIdReturnNotNullAndNameUser()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUser(
                new NewUserDTO("Neusa Boaz", "neusa@email.com", "134652", "URLFOTO" ,TypeUser.Commom)
            );

            //WHEN - Quando pesquiso pelo id 1
            var user = _repository.GetUserById(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }

        [TestMethod]
        public void UpdateUserReturnUserUpdated()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUser(
                new NewUserDTO("Estefânia Boaz", "estefania@email.com", "134652", "URLFOTO", TypeUser.Commom)
            );

            //WHEN - Quando atualizamos o usuario
            _repository.AttUser(
                new UpdateUserDTO(1, "Estefânia Moura", "123456", "URLFOTONOVA", TypeUser.Commom)
            );

            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            var antigo = _repository.GetUserByEmail("estefania@email.com");

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