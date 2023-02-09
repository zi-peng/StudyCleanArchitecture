using Study.CleanArchitecture.Application.Interfaces;

namespace Study.CleanArchitecture.Infrastructure.Data.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
