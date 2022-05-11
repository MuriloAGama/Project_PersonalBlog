using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using PersonalBlog.src.repositories;
using PersonalBlog.src.services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalBlog.src.servicos.implementations
{
    public class AuthenticationServices : IAuthentication
    {
        #region Attribute
        private readonly IUser _repository;
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructor
        public AuthenticationServices(IUser repository, IConfiguration
        configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }
        #endregion

        #region Methods
        public string EncodePassword(string password)
        {

            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);

        }

        public void CreateUserNotDuplicate(NewUserDTO dto)
        {

            var user = _repository.GetUserByEmail(dto.Email);
            if (user != null) throw new Exception("This email is already being used");
            dto.Password = EncodePassword(dto.Password);
            _repository.AddUser(dto);
        }



        public string GenerateToken(UserModel user)
        {
            var tokenManipulator = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
            new Claim[]
            {
            new Claim(ClaimTypes.Email, user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.Type.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenManipulator.CreateToken(tokenDescricao);
            return tokenManipulator.WriteToken(token);
        }

        public AuthorizationDTO GetAuthorization(AuthenticationDTO authentication)
        {
            var user = _repository.GetUserByEmail(authentication.Email);
            if (user == null) throw new Exception("Use not found");
            if (user.Password != EncodePassword(authentication.Password)) throw new
            Exception("Incorrect password");
            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GenerateToken(user));
        }

        #endregion
    }
}


