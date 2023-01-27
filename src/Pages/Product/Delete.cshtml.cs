using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Behavior for dedicated Delete page
    /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middle-tier
        private JsonFileProductService ProductService { get; }

        // The data to show, bind to it for the post
        [BindProperty]
        public Models.ProductModel Product { get; set; } = null;

        /// <summary>
        /// Constructor for the DeleteModel
        /// </summary>
        /// <param name="productService"> Access to a JsonFileProductService</param>
        public DeleteModel(JsonFileProductService productService)
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
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));

            // Redirect to error page if no matching product is found
            if (Product == null)
            {
                var message = "The requested id=" + id + " was not found";
                return RedirectToPage("/Error", new { errorMsg = message });
            }

            return Page();
        }

        /// <summary>
        /// Deletes the Product and redirects the user back to index
        /// </summary>
        /// <returns>
        /// Sends the user to the Success page
        /// </returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.DeleteData(Product.Id);

            return RedirectToPage("/Success");
        }
    }
}