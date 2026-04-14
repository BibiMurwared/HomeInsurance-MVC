/*
 * File: ILogService.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-14
 * Description:
 * This interface defines logging operations for the application.
 * It separates logging behavior from implementation, allowing
 * controllers and services to log user actions and system errors
 * without directly accessing the database.
 */

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
