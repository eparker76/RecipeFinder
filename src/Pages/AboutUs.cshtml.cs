using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// AboutUs model class for the page view
    /// </summary>
    public class AboutUsModel : PageModel
    {
        // Readonly logger - created logger for ContactUs
        private readonly ILogger<AboutUsModel> _logger;

        /// <summary>
        /// Constructor for the AboutUsModel class.
        /// </summary>
        /// <param name="logger">Instance of application logger</param>
        public AboutUsModel(ILogger<AboutUsModel> logger)
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