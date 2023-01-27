using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// The model for the read CRUDi page
    /// </summary>
    public class ReadModel : PageModel
    {
        // Data middle-tier
        private JsonFileProductService ProductService { get; }

        // The current product being read
        public ProductModel product { get; set; }

        /// <summary>
        /// Constructor for the ReadModel class.
        /// </summary>
        /// <param name="productService">Service for interfacing with the product records</param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Triggered on navigation to retrieve the sought product record.
        /// </summary>
        /// <param name="id">Identifier of the sought product record</param>
        /// <returns>Current or redirected page</returns>
        public IActionResult OnGet(string id)
        {
            // If id empty or null redirect user to index
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("Product/Index");
            }

            // Retrieves the sought product
            product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));

            // Redirect to error page if no matching product is found
            if (product == null)
            {
                var message = "The requested id=" + id + " was not found";
                return RedirectToPage("/Error", new { errorMsg = message});
            }

            return Page();
        }
    }
}