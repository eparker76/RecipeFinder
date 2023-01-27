using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.Language;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// The JSON File Product Service allows the user to interact with the JSON
    /// file that serves as a database in this project
    /// </summary>
   public class JsonFileProductService
    {
        // Web Host Environment 
        public IWebHostEnvironment WebHostEnvironment { get; }

        // Text entered into search box
        public string SearchText = "";

        // Current page number
        public int page { get; set; } = 1;

        // Items per page
        public int pageSize { get; set; } = 5;

        // The column to designate for sorting
        public string sortColumn { get; set; } = "Title";

        // The direction, in which records are sorted by the sort column value
        public bool sortAsc { get; set; } = false;

        // Selected ingredient value
        public IEnumerable<int> IngredientNumbers = Enumerable.Empty<int>();

        // Selected producttype value
        public IEnumerable<int> ProductTypeNumbers = Enumerable.Empty<int>();

        // Json file containing records for products
        private string ProductJsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// Constructor for the JsonFileProductService
        /// </summary>
        /// <param name="webHostEnvironment">Instance of web hosting environment</param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
                
        /// <summary>
        /// Get All Data helper function returns all data in the JSON file 
        /// as an IEnumerable Product Model object
        /// </summary>
        /// <returns>All product records</returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            using(var jsonFileReader = File.OpenText(ProductJsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Get a single product info using its ID
        /// </summary>
        /// <param name="id">>Identifier of the sought product</param>
        /// <returns>The sought product record</returns>
        public ProductModel GetProductById(string id)
        {
            try
            {
                // Grab the product that matches our input id
                var data = GetAllData().Where(x => x.Id == id);
                ProductModel singleProduct = data.ElementAt(0);
                return singleProduct;
            }

            catch (ArgumentOutOfRangeException)
            {
                // If the id passed is invalid, we return null.
                return null;
            }
        }

        /// <summary>
        /// Add Rating
        /// Take in the product ID and the rating
        /// If the rating does not exist, add it
        /// Save the update
        /// </summary>
        /// <param name="productId">Product ID of the item in question</param>
        /// <param name="rating">Star rating chosen by the user</param>
        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return
            if (productId == null)
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if(data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(products);

            return true;
        }
       
        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data">The Product data that will be the object's new state</param>
        public ProductModel UpdateData(ProductModel data)
        {
            var products = GetAllData();
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }
            
            // Update the data to the new passed in values
            //  EXCEPT for comments and ratings which are not changed
            productData.Title = data.Title;
            productData.Maker = data.Maker;
            productData.Description = data.Description.Trim();
            productData.Url = data.Url;
            productData.Image = data.Image;            
            productData.Ingredients = data.Ingredients;
            productData.Instructions = data.Instructions;
            productData.TimeStamp = DateTime.Now.ToString();

            SaveData(products);

            return productData;
        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        /// <param name="products">IEnumerable list of Products</param>
        private void SaveData(IEnumerable<ProductModel> products)
        {

            using (var outputStream = File.Create(ProductJsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns><The created record/returns>
        public ProductModel CreateData()
        {
            // Default Values
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter URL",
                Image = "",
                Instructions = "",
            };
            //Time Stamp new Product
            data.TimeStamp = DateTime.Now.ToString();
            

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(data);

            // Save data set with new Product added
            SaveData(dataSet);

            return data;
        }

        /// <summary>
        /// Add a product using to the existing dataset
        /// </summary>
        /// <param name="input">The product record to add</param>
        /// <returns>The added record</returns>
        public ProductModel AddData(ProductModel input)
        {
            //Reset Time Stamp
            input.TimeStamp = DateTime.Now.ToString();

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(input);

            // Save data set with new Product added
            SaveData(dataSet);

            return input;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <param name="id">The identifier of the product to delete</param>
        /// <returns>The deleted record</returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);
            
            SaveData(newDataSet);

            return data;
        }

        /// <summary>
        /// Searches for ingredients in each recipe
        /// </summary>
        /// <returns>
        /// Returns a list of recipes that have the ingredient
        /// </returns>
        public IEnumerable<ProductModel> SearchIngredients()
        {
            // Gets all the recipes
            var products = GetAllData();

            // If no ingredient is selected, display all the recipes
            if (IngredientNumbers.Count() == 0)
            {
                return products;
            }

            var results = new List<ProductModel>();

            // Searches each recipe for matching ingredients. Does not leverage LINQ since the
            // required select statement is not covered in unit test coverage.
            foreach (var product in products)
            {
                var matchingIngredients = new List<int>();

                // Retrieve ingredients with an amount > 0 specified
                foreach (var ingredient in product.Ingredients)
                {
                    if (ingredient.Value > 0)
                    {
                        matchingIngredients.Add(ingredient.Key);
                    }
                }

                // Add the product if the intersected list contains all of the ingredient filters
                if (matchingIngredients.AsQueryable().Intersect(IngredientNumbers).Count() == IngredientNumbers.Count())
                {
                    results.Add(product);
                }
            }

            return results.AsEnumerable();
        }

        /// <summary>
        /// Searches for ProductTypes in each recipe
        /// </summary>
        /// <returns>
        /// Returns a list of recipe that have the product type
        /// </returns>
        public IEnumerable<ProductModel> SearchProductType()
        {
            // Gets all the recipes
            var products = SearchIngredients();

            // If no producttype is selected, display all the recipes
            if (ProductTypeNumbers.Count() == 0)
            {
                return products;
            }

            // Searches each recipe for the producttype
            var results = products.Where(
                x => x.ProductType.Intersect(ProductTypeNumbers).Count() == ProductTypeNumbers.Count());

            return results;
        }

        /// <summary>
        /// Retrieves the sorted collection of products.
        /// </summary>
        /// <param name="products">The collection of products to sort</param>
        /// <returns>The sorted products</returns>
        public IEnumerable<ProductModel> SortData(IEnumerable<ProductModel> products)
        {
            if (sortAsc)
            {
                return products.OrderBy(x => x.GetType().GetProperty(sortColumn).GetValue(x, null));
            }

            return products.OrderByDescending(x => x.GetType().GetProperty(sortColumn).GetValue(x, null));
        }

        /// <summary>
        /// Searches for recipes and ingredients in the data
        /// </summary>
        /// <returns>
        /// Recipes found in the search
        /// </returns>
        public IEnumerable<ProductModel> SearchData()
        {
            // Get list of all products with the specified ingredients
            var products = SearchProductType();

            // Sort products based on configured sort parameters
            products = SortData(products);

            // Return all products if no filter
            if (string.IsNullOrEmpty(SearchText))
            {
                return products;
            }

            // Identifying matching records
            var recipeResults = products.Where(x => x.Title.Contains(
                SearchText, StringComparison.CurrentCultureIgnoreCase));

            return recipeResults;
        }

        /// <summary>
        /// Retrieves the max number of pages in the paginated record set.
        /// </summary>
        /// <returns>Maximum number of paginated pages</returns>
        public int GetMaxPages()
        {
            // Total number of recipes
            var totalRecords = SearchData().Count();

            // Calculates the total number of pages
            var numPages = Math.Ceiling(totalRecords / (double)pageSize);

            return (int)numPages;
        }

        /// <summary>
        /// Divide database into pages of a standard size for the index
        /// </summary>
        /// <returns>The paginated set of records</returns>
        public IEnumerable<ProductModel> Paginate()
        {
            // Get all pages, and display the appropriate few based on the current page
            var products = SearchData();

            // Calculate the number of records to skip
            var offset = (page - 1) * pageSize;

            return products.Skip(offset).Take(pageSize);
        }
    }
}