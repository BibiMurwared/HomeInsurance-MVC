namespace HomeInsurance_MVC.Services
{
    using HomeInsurance_MVC.Data;
    using HomeInsurance_MVC.Models;

    /// <summary>
    /// Stores log records into database tables.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly HomeInventoryContext _context;

        public LogService(HomeInventoryContext context)
        {
            _context = context;
        }

        public async Task LogUserEventAsync(Guid? userId, string action, string? details, bool isSuccess)
        {
            UserLog userLog = new UserLog
            {
                UserLogID = Guid.NewGuid(),
                UserID = userId,
                Action = action,
                Details = details,
                IsSuccess = isSuccess,
                CreatedDate = DateTime.UtcNow
            };

            await _context.UserLogs.AddAsync(userLog);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task LogSystemErrorAsync(string path, string message, string? stackTrace)
        {
            SystemLog systemLog = new SystemLog
            {
                SystemLogID = Guid.NewGuid(),
                Path = path,
                Message = message,
                StackTrace = stackTrace,
                CreatedDate = DateTime.UtcNow
            };

            await _context.SystemLogs.AddAsync(systemLog);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
