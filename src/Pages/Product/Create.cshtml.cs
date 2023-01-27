using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;
using System;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Behavior for dedicated Create page
    /// </summary>
    public class CreateModel : PageModel
    {
        // Data middle-tier
        private JsonFileProductService ProductService { get; }

        // The data to show, bind to it for the post
        [BindProperty]
        public Models.ProductModel Product { get; set; }


        /// <summary>
        /// Constructor for the CreateModel
        /// </summary>
        /// <param name="productService"> Access to a JsonFileProductService</param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;

            // Store a container of ingredients mapped to their initial amounts (i.e. 0)
            var ingredients = new Dictionary<int, int>();

            // Populate ingredients dictionary with available ingredients and initial amounts
            foreach (IngredientTypeEnum ingredientType in Enum.GetValues(typeof(IngredientTypeEnum)))
            {
                int ingredientValue = (int)ingredientType;

                if (ingredientValue > 0)
                {
                    ingredients.Add(ingredientValue, 0);
                }
            }

            // Initial product model
            Product = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = "",
                Instructions = "",
                Ingredients = ingredients
            };
        }

        /// <summary>
        /// Invoked upon navigation to the page
        /// </summary>
        /// <returns>Redirects to Update page for new Product ID</returns>
        public IActionResult OnGet()
        {
            // Create a new Product with default values, then direct to Update
            return Page();
        }


        /// <summary>
        /// Invoked on submission of the Create page
        /// </summary>
        /// <returns>Sends the user to the Success page if a valid product was created</returns>
        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine(Product.ToString());

            // Check for Valid Product
            if (validProduct())
            {
                ProductService.AddData(Product);
                return RedirectToPage("/Success");
            }
            // Otherwise, return
            return Page();
        }

        /// <summary>
        /// Used to check if the user filled out the Create form properly
        /// </summary>
        /// <returns>Reurns true if the Title is unchanged, or deleted</returns>
        public bool validProduct()
        {
            if (Product.Title == "Enter Title")
            {
                return false;
            }
            else if (Product.Title == "")
            {
                return false;
            }
            return true;
        }
    }
}