using minmalAPI.AppDB;
using minmalAPI.DTOs;
using minmalAPI.Entities;

namespace minmalAPI.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<MinimalDTO>> GetAllAsync();
        Task CreateAsync(MinimalDTO create);
        Task UpdateAsync(UpdateMinimalDTO update);
        Task<TodoModel> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(int id);
    }
}
