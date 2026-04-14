/*
 * File: UserLog.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-14
 * Description:
 * This model represents user activity logs. It records important actions
 * performed by users such as login attempts, item creation, updates,
 * deletions, and other system interactions. These logs support auditing
 * and tracking user behavior.
 */

namespace HomeInsurance_MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Stores user activity logs.
    /// </summary>
    public class UserLog
    {
        [Key]
        public Guid UserLogID { get; set; }

        public Guid? UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Details { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
