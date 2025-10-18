using Microsoft.EntityFrameworkCore;
using minmalAPI.Entities;

namespace minmalAPI.AppDB
{
    public class AppDBContext(DbContextOptions<AppDBContext> option) : DbContext(option)
    {
        public DbSet<TodoModel> todoModels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
