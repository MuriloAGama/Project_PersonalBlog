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
using System.Threading.Tasks;

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

        /// <summary>
        /// <para>Resumo: Método responsavel por criptografar senha</para>
        /// </summary>
        /// <param name="password">Senha a ser criptografada</param>
        /// <returns>string</returns>
        public string EncodePassword(string password)
        {

            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);

        }

        /// <summary>
        /// <para>Resumo: Método assíncrono responsavel por criar usuario sem duplicar no banco</para>
        /// </summary>
        /// <param name="dto">NovoUsuarioDTO</param>
        public async Task CreateUserNotDuplicateAsync(NewUserDTO dto)
        {

            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user != null) throw new Exception("This email is already being used");

            dto.Password = EncodePassword(dto.Password);

            await _repository.AddUserAsync(dto);
        }


        /// <summary>
        /// <para>Resumo: Método responsavel por gerar token JWT</para>
        /// </summary>
        /// <param name="user">UsuarioModelo</param>
        /// <returns>string</returns>
        public string GenerateToken(UserModel user)
        {
            var tokenManipulator = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
            new Claim[]
            {
            new Claim(ClaimTypes.Email, user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.Type.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenManipulator.CreateToken(tokenDescription);
            return tokenManipulator.WriteToken(token);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono responsavel devolver autorização para usuario autenticado</para>
        /// </summary>
        /// <param name="authentication">AutenticarDTO</param>
        /// <returns>AutorizacaoDTO</returns>
        /// <exception cref="Exception">Usuário não encontrado</exception>
        /// <exception cref="Exception">Senha incorreta</exception>
        public async Task <AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication)
        {
            var user = await _repository.GetUserByEmailAsync(authentication.Email);
            if (user == null) throw new Exception("Use not found");
            if (user.Password != EncodePassword(authentication.Password)) throw new
            Exception("Incorrect password");
            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GenerateToken(user));
        }

        #endregion
    }
}


