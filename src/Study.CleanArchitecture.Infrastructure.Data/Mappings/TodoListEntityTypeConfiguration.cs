using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;
using Study.CleanArchitecture.Domain.ValueObjects;

namespace Study.CleanArchitecture.Infrastructure.Data.Mappings;

public class TodoListEntityTypeConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.ToTable("todo_list");
        builder.Property(p => p.Colour).HasConversion(v=>v.ToString(), v => (Colour)Enum.Parse(typeof(Colour), v)).HasMaxLength(20);
    }
}