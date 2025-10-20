using Microsoft.EntityFrameworkCore;
using minmalAPI.AppDB;
using minmalAPI.DTOs;
using minmalAPI.Entities;

namespace minmalAPI.Services
{
    public class TodoService(AppDBContext _context, ILogger<TodoService> _logger) : ITodoService
    {

        public async Task CreateAsync(MinimalDTO modelDTO)
        {
            try
            {
                TodoModel model = new TodoModel();
                model.Description = modelDTO.Description;
                model.IsComplete = modelDTO.IsComplete;
                await _context.todoModels.AddAsync(model);
                await _context.SaveChangesAsync();
                _logger.LogInformation("CreateAsync: Creating a new Todo item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateAsync: An error occurred while creating a new Todo item.");
                throw;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                TodoModel? model = await GetByIdAsync(id);
                if (model == null)
                {
                    _logger.LogWarning("DeleteByIdAsync: Todo with ID {Id} not found.", id);
                    return false;
                }

                _context.todoModels.Remove(model);
                await _context.SaveChangesAsync();
                _logger.LogInformation("DeleteByIdAsync: Todo with ID {Id} was successfully deleted.", id);

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteByIdAsync: An error occurred while deleting Todo with ID {Id}.", id);
                return false;
            }

        }

        public async Task<IEnumerable<MinimalDTO>> GetAllAsync()
        {
            try
            {
                List<MinimalDTO> model = await _context.todoModels.Select(t => new MinimalDTO
                {
                    Description = t.Description,
                    IsComplete = t.IsComplete
                }).ToListAsync();
                _logger.LogInformation("GetAllAsync: Successfully retrieved {Count} Todo items.", model.Count);

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync: An error occurred while retrieving Todo items.");

                return null;
            }

        }

        public async Task<TodoModel> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("UpdateAsync: Todo with ID {Id} was successfully Update.", id);

                return await _context.todoModels.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync: An error occurred while Update Todo with ID {Id}.", id);

                return null;
            }
        }

        public async Task UpdateAsync(UpdateMinimalDTO update)
        {

            try
            {
                TodoModel model = await GetByIdAsync(update.Id);

                if (model == null)
                {
                    _logger.LogWarning("UpdateAsync: Todo with ID {Id} not found.", update.Id);

                    return;
                }
                model.IsComplete = update.IsComplete;
                model.Description = update.Description;

                _context.todoModels.Update(model);
                await _context.SaveChangesAsync();

                _logger.LogInformation("UpdateAsync: Todo with ID {Id} was successfully Update.", update.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync: An error occurred while Update Todo with ID {Id}.", update.Id);

                return;

            }
        }

    }
}
