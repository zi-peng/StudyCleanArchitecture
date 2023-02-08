using System.Globalization;
using CsvHelper.Configuration;
using Study.CleanArchitecture.Application.Services.TodoLists.Queries.ExportTodos;

namespace Study.CleanArchitecture.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
