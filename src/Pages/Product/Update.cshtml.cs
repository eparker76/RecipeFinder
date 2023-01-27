using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using System.Reflection.PortableExecutable;
using System;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Dedicated Update Page Behavior
    /// Based on CRUDi presetation Slide 28
    /// </summary>
    public class UpdateModel : PageModel
    {

        // Data middle-tier
        private JsonFileProductService ProductService { get; }

        // The data to show, bind to it for the post
        [BindProperty]
        public Models.ProductModel Product { get; set; } = null;

        /// <summary>
        /// Constructor for the UpdateModel
        /// </summary>
        /// <param name="productService">
        /// Connection to service for interacting with JSON files
        /// </param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // <summary>
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

            // Populate ingredients dictionary with available ingredients and initial amounts if
            // they currently don't exist in the ingredient dictionary
            foreach (IngredientTypeEnum ingredientType in Enum.GetValues(typeof(IngredientTypeEnum)))
            {
                int ingredientValue = (int)ingredientType;

                // Indicates an unknown ingredient
                if (ingredientValue == 0)
                {
                    continue;
                }
                // Indicates that the ingredient is missing from the recipe record
                else if (!Product.Ingredients.ContainsKey(ingredientValue))
                {
                    Product.Ingredients.Add(ingredientValue, 0);
                }
            }

            return Page();
        }

        /// <summary>
        /// Response for POST request
        /// </summary>
        /// <returns> Sends the user to the Success page if the update is valid, or stays if invalid</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductService.UpdateData(Product);
            return RedirectToPage("/Success");
        }
    }
}