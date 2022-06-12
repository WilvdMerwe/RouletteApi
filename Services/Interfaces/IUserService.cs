using RouletteApi.Models;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;

namespace RouletteApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<RouletteResponse> Create(UserCreateRequest request);
        Task<RouletteResponse<IEnumerable<User>>> GetAll();
        Task<RouletteResponse<User>> GetById(int id);
        Task<RouletteResponse<User>> Update(int id, UserUpdateRequest request);
        Task<RouletteResponse> DeleteById(int id);
    }
}
