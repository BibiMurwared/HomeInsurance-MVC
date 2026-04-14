/*
 * File: SystemLog.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-14
 * Description:
 * This model represents system-level error logs. It is used to capture
 * unexpected exceptions and runtime failures occurring within the application.
 * These logs help developers diagnose issues during development and after deployment.
 */

namespace HomeInsurance_MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Stores system error logs.
    /// </summary>
    public class SystemLog
    {
        [Key]
        public Guid SystemLogID { get; set; }

        [Required]
        [StringLength(300)]
        public string Path { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        public string? StackTrace { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
