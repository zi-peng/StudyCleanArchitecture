using System.Globalization;
using CsvHelper;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Application.Services.TodoLists.Queries.ExportTodos;

namespace Study.CleanArchitecture.Infrastructure.Shared.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
