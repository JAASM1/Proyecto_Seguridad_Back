using back_sistema_de_eventos.Context;
using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;
using back_sistema_de_eventos.Services.IService;
using back_sistema_de_eventos.Services.IService.IUser;
using Microsoft.EntityFrameworkCore;

namespace back_sistema_de_eventos.Services
{
    public class UserService : IUserService

    {
       private readonly ApplicationDBContext _DbContext;

        public UserService(ApplicationDBContext context) 
        { 
            _DbContext = context;

        }

        //Traer todo los usuarios
        public List<User> GetUser() 
        {
            return _DbContext.Users.ToList();
        }

        //Traer los usuarios por ID
        public async Task<User> GetUserById(int id)
        {
            return await _DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        //Crear Usuario
        public async Task<bool> CreateUser(UserCreateDTO request)
        {
            try
            {
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                };

                _DbContext.Users.Add(user);
                return await _DbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message, ex);
            }
        }

        // Editar usuario
        public async Task<bool> EditUser(int Id, UserEditDTO request)
        {
            try
            {
                var user = await _DbContext.Users.FindAsync(Id);
                if (user == null) return false;

                user.Name = request.Name ?? user.Name;
                user.Email = request.Email ?? user.Email;

                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }

                _DbContext.Users.Update(user);
                return await _DbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message, ex);
            }
        }


        // Eliminar usuario
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _DbContext.Users.FindAsync(id);
            if (user == null) return false;

            _DbContext.Users.Remove(user);
            return await _DbContext.SaveChangesAsync() > 0;
        }

    }
}
