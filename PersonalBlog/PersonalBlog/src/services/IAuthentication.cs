using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Threading.Tasks;

namespace PersonalBlog.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserNotDuplicateAsync(NewUserDTO user);
        string GenerateToken(UserModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication);
    }
}


