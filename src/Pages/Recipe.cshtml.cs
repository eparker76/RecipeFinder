using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Recipe model class, support recipe index view
    /// </summary>
    public class RecipeModel : PageModel
    {
        // Data middle-tier
        private JsonFileProductService ProductService { get; }

        // Initialize current average rating to be displayed to user.
        public int avgRating = 0;

        // Initialize current vote count to be displayed to user.
        public int voteCount = 0;

        // Denote "votes" or "vote" to be displayed to user.
        public string voteLabel;

        // The identifier of the selected product
        public string selectedProductId;

        // The current rating of the product
        public int currentRating = 0;

        // The data to show, bind to it for the post
        [BindProperty]
        public Models.ProductModel Product { get; set; } = null;

        [BindProperty]
        // User input rating.
        public int rating { get; set; } = 0;

        /// <summary>
        /// Constructor for the Recipe page
        /// </summary>
        /// <param name="productService">The service for interfacing with products</param>
        public RecipeModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Get current average rating and vote count for the current product to display to user.
        /// </summary>
        private void GetCurrentRating()
        {
            if (Product.Ratings == null)
            {
                currentRating = 0;
                voteCount = 0;
                System.Console.WriteLine($"Current rating for {Product.Id}: {currentRating}");
                return;
            }
           
            voteCount = Product.Ratings.Count();
            voteLabel = voteCount > 1 ? "Votes" : "Vote";
            currentRating = Product.Ratings.Sum() / voteCount;          
            System.Console.WriteLine($"Current rating for {Product.Id}: {currentRating}");
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

            // Update current rating 
            GetCurrentRating();

            return Page();
        }

        /// <summary>
        /// Invoked upon the assignment of new ratings to the product.
        /// </summary>
        /// <param name="id">Identifier of the product record</param>
        public void OnPost(string id)
        {
            // Assign the user's selected product to the current product var.
            Product = ProductService.GetProductById(id);

            if (rating != 0)
            {
                // Add Rating to product model.
                ProductService.AddRating(Product.Id, rating);
                GetCurrentRating();
                Redirect("/Product/Read/" + id.ToString() + "#top");
            }
        }
    }
}
