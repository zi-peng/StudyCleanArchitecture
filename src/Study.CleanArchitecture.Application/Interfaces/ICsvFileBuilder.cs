using Study.CleanArchitecture.Application.Services.TodoLists.Queries.ExportTodos;

namespace Study.CleanArchitecture.Application.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
