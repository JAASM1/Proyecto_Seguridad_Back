using back_sistema_de_eventos.Models.App;
using back_sistema_de_eventos.Models.DTOs;

namespace back_sistema_de_eventos.Services.IService.IUser
{
    public interface IUserService
    {
        public List<User> GetUser();
        public Task<User> GetUserById(int id);
        //public Task<bool> CreateUser(UserCreateDTO request);

        public Task<bool> EditUser(int Id, UserEditDTO request);
        public Task<bool> DeleteUser(int id);
    }
}
