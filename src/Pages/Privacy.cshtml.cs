using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Privacy model class for the page view
    /// </summary>
    public class PrivacyModel : PageModel
    {
        // Readonly logger - created logger for PrivacyModel
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Constructor for the PrivacyModel class.
        /// </summary>
        /// <param name="logger">Instance of application logger</param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Resonse for Get request
        /// </summary>
        public void OnGet()
        {
        }
    }
}