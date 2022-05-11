using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;

namespace PersonalBlog.src.repositories.implementations
{
    public class UserRepository : IUser
    {
       
        #region Attributes

        private readonly PersonalBlogContext _context;

        #endregion Attributes

        #region Constructor

        public UserRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public void AddUser(NewUserDTO newUsuario)
        {
            _context.Users.Add(new UserModel
            {
                Email = newUsuario.Email,
                Name = newUsuario.Name,
                Password = newUsuario.Password,
                Photograph = newUsuario.Photograph,
            }); 

            _context.SaveChanges();
        }
        public void AttUser(UpdateUserDTO UpdateUser)
        {
            var existingUser = GetUserById(UpdateUser.Id);
            existingUser.Name = UpdateUser.Name;
            existingUser.Password = UpdateUser.Password;
            existingUser.Photograph = UpdateUser.Photograph;
            _context.Users.Update(existingUser);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<UserModel> GetUserByName(string name)
        {
            return _context.Users.Where(u => u.Name == name).ToList();
        }

            #endregion Methods
    }
}
