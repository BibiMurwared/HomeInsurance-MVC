/*
 * File: ErrorViewModel.cs
 * Project: HomeInsurance-MVC
 * Author(s): Mohammad
 * Date: 2026-04-13
 * Description:
 * This model stores error-related information used by the shared error page.
 * It helps display request identifiers and supports basic debugging.
 */

namespace HomeInsurance_MVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
