using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Index model class for main index view
    /// </summary>
    public class IndexModel : PageModel
    {
        // Holds service for interfacing with products
        public JsonFileProductService ProductService { get; }

        // Holds all product records
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// Constructor for the IndexModel
        /// </summary>
        /// <param name="productService">Service for interfacing with products</param>
        public IndexModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Invoked upon navigation to the page
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetAllData();
        }
    }
}