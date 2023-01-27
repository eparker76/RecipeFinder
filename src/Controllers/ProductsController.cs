using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// API Controller to communicate between back-end and user interface
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService">The service for interfacing with products</param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Provides a connection to the ProductService
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Response for a GET request is to send all Product data
        /// </summary>
        /// <returns>All of the product records</returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }

        /// <summary>
        /// Reponse for a PATCH request is to Add Rating and send response
        /// </summary>
        /// <param name="request">New ratings information to create</param>
        /// <returns>A HTTP 200 response</returns>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }

        /// <summary>
        /// Rating Request to add user's rating to pool of existing ratings
        /// </summary>
        public class RatingRequest
        {
            // Product ID for the item to be rated
            public string ProductId { get; set; }

            // Rating from the user interface
            public int Rating { get; set; }
        }
    }
}