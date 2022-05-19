using Microsoft.EntityFrameworkCore;
using PersonalBlog.src.data;
using PersonalBlog.src.dtos;
using PersonalBlog.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AddUserAsync(NewUserDTO newUsuario)
        {
            await _context.Users.AddAsync(new UserModel
            {
                Email = newUsuario.Email,
                Name = newUsuario.Name,
                Password = newUsuario.Password,
                Photograph = newUsuario.Photograph,
            }); 

            await _context.SaveChangesAsync();
        }
        public async Task AttUserAsync(UpdateUserDTO UpdateUser)
        {
            var existingUser = await GetUserByIdAsync(UpdateUser.Id);
            existingUser.Name = UpdateUser.Name;
            existingUser.Password = UpdateUser.Password;
            existingUser.Photograph = UpdateUser.Photograph;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserModel>> GetUserByNameAsync(string name)
        {
            return await _context.Users
                .Where(u => u.Name == name)
                .ToListAsync();
        }

            #endregion Methods
    }
}
