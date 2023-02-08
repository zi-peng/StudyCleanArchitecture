using Study.CleanArchitecture.Application.Interfaces;

namespace Study.CleanArchitecture.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
