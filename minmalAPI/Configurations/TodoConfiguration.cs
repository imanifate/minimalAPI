using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging.Abstractions;
using minmalAPI.Entities;

namespace minmalAPI.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoModel>
    {
        public void Configure(EntityTypeBuilder<TodoModel> builder)
        {
           
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(128);

        }
    }
}
