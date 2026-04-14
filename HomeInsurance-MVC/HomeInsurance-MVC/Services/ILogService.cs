namespace HomeInsurance_MVC.Services
{
    /// <summary>
    /// Provides logging operations for user and system events.
    /// </summary>
    public interface ILogService
    {
        Task LogUserEventAsync(Guid? userId, string action, string? details, bool isSuccess);

        Task LogSystemErrorAsync(string path, string message, string? stackTrace);
    }
}
