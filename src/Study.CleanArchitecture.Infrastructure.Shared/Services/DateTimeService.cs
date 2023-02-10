using Study.CleanArchitecture.Application.Interfaces;

namespace Study.CleanArchitecture.Infrastructure.Shared.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
