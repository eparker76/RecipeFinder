using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents the Error page
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // Request identifier to display
        public string RequestId { get; set; }

        // The error to display on the page
        public string ErrorMessage { get; set; }

        // Show requested id - return false if string, null or empty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Readonly logger for ErrorModel page
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Constructor for the ErrorModel
        /// </summary>
        /// <param name="logger">Instance of application logger</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Invoked upon navigation to the page
        /// </summary>
        /// <param name="errorMsg">Error message to display, otherwise null.</param>
        public void OnGet(string ?errorMsg)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            if (errorMsg != null)
            {
                ErrorMessage = errorMsg;
            }
        }
    }
}