using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using minmalAPI.AppDB;
using minmalAPI.DTOs;
using minmalAPI.Entities;

namespace minmalAPI.Services
{
    public class TodoService (AppDBContext _context): ITodoService
    {
       
        public async Task CreateAsync(MinimalDTO modelDTO)
        {
            TodoModel model = new TodoModel();
            model.Description = modelDTO.Description;
            model.IsComplete = modelDTO.IsComplete;
            _context.todoModels.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MinimalDTO>> GetAllAsync()
        {
        List<MinimalDTO> model = await _context.todoModels.Select(t => new MinimalDTO
          {
              Description = t.Description,
              IsComplete = t.IsComplete
          }).ToListAsync();
            return model; 
        }

        public async Task<TodoModel> GetByIdAsync(int id)
        {
            if(id == null) return null;
            try
            {
                return await _context.todoModels.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }           
        }

        public async Task UpdateAsync(UpdateMinimalDTO update )
        {

            try
            {
                TodoModel model = await GetByIdAsync(update.Id);

                if (model == null)
                {
                 
                    return; 
                }
                model.IsComplete = update.IsComplete;
                model.Description = update.Description;

                _context.todoModels.Update(model);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
               
                return ; 

            }
        }

    }
}
