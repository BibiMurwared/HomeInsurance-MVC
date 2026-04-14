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
