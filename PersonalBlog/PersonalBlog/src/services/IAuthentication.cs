using PersonalBlog.src.dtos;
using PersonalBlog.src.models;

namespace PersonalBlog.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        void CreateUserNotDuplicate(NewUserDTO user);
        string GenerateToken(UserModel user);
        AuthorizationDTO GetAuthorization(AuthenticationDTO authentication);
    }
}


