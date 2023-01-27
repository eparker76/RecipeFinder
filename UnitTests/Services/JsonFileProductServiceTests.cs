using System.Linq;
using NUnit.Framework;
using System;
using ContosoCrafts.WebSite.Models;
using Newtonsoft.Json.Linq;

namespace UnitTests.Pages.Product
{
    /// <summary>
    /// Tests for the JsonFileProductService class.
    /// </summary>
    public class JsonFileProductServiceTests
    {
        #region TestSetup
        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup

        #region GetProductById
        /// <summary>
        /// Validates that retrieving a product with an identifier of null will return the value
        /// null.
        /// </summary>
        [Test]
        public void GetProductById_Invalid_Product_Null_Should_Return_Null()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.GetProductById(null);

            // Assert
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Validates that retrieving a product with an identifier of null will return the value
        /// null.
        /// </summary>
        [Test]
        public void GetProductById_Valid_Product_Should_Return_Product()
        {
            // Arrange

            // Get the First data item
            var product = TestHelper.ProductService.GetAllData().First();

            // Act
            var result = TestHelper.ProductService.GetProductById(product.Id);

            // Assert
            Assert.AreEqual(product.Id, result.Id);
        }
        #endregion GetProductById

        #region AddRating
        /// <summary>
        /// Validates that adding an invalid rating with a null identifier will return the value
        /// false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Validates that adding an invalid rating with an empty identifier will return the value 
        /// false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Validates that adding a rating to an identifier that does not exist will return the 
        /// value false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_NotFound_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("-1", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Validates that adding a rating less than the minimum allowed value will return the
        /// value false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_LessThan0_Should_Return_False()
        {
            // Arrange

            // Get the First data item that has ratings
            var data = TestHelper.ProductService.GetAllData().Where(
                x => x.Ratings != null).First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, -1);
            var dataNewList = TestHelper.ProductService.GetAllData().Where(
                x => x.Id == data.Id).First();

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(countOriginal, dataNewList.Ratings.Length);
        }

        /// <summary>
        /// Validates that adding a rating greater than the maximum allowed value will return the
        /// value false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_GreaterThan5_Should_Return_False()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().Where(
                x => x.Ratings != null).First();
            int countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 6);
            var dataNewList = TestHelper.ProductService.GetAllData().Where(
                x => x.Id == data.Id).First();
            var dataNewListCount = dataNewList.Ratings.Length;

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(countOriginal, dataNewListCount);
        }

        /// <summary>
        /// Validates that adding a rating between 0 and 5 to a valid product identifier will 
        /// return the value true.
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().Where(x => x.Ratings != null).First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().Where(
                x => x.Id == data.Id).First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }

        /// <summary>
        /// Validates that adding a rating to product that was not previously rated will construct
        /// a ratings array and return the value true.
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_NotPreviouslyRated_Rating_5_Should_Return_True()
        {
            // Arrange
            var data = TestHelper.ProductService.CreateData();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().FirstOrDefault(
                x => x.Id.Equals(data.Id)
            );

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }
        #endregion AddRating

        #region DeleteData
        /// <summary>
        /// Validates that deleting a product with a valid identifier will return the value true
        /// </summary>
        [Test]
        public void DeleteData_Valid_Product_Should_Return_Product()
        {
            // Arrange

            // Get the first data item and products count
            var countOriginal = TestHelper.ProductService.GetAllData().Count();
            var product = TestHelper.ProductService.GetAllData().First();

            // Act
            var result = TestHelper.ProductService.DeleteData(product.Id);
            var countModified = TestHelper.ProductService.GetAllData().Count();

            // Assert
            Assert.AreEqual(product.Id, result.Id);
            Assert.AreEqual(countOriginal - 1, countModified);
        }
        #endregion DeleteData

        #region SortData
        /// <summary>
        /// Validates that sorting products by name in ascending order has the expected behavior.
        /// </summary>
        [Test]
        public void SortData_Valid_NameAsc_Should_Return_Products()
        {
            // Arrange
            TestHelper.ProductService.SearchText = "";
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();
            TestHelper.ProductService.sortColumn = "Title";
            TestHelper.ProductService.sortAsc = true;

            // Get initial products
            var products = TestHelper.ProductService.GetAllData();

            // Act
            var result = TestHelper.ProductService.SortData(products);
            
            // Get first and last products to compare title ordering
            var firstProduct = result.First();
            var lastProduct = result.Last();

            // Reset
            TestHelper.ProductService.sortAsc = false;

            // Assert
            Assert.AreEqual(true, string.Compare(firstProduct.Title, lastProduct.Title) < 0);
        }

        /// <summary>
        /// Validates that sorting products by name in descending order has the expected behavior.
        /// </summary>
        [Test]
        public void SortData_Valid_NameDesc_Should_Return_Products()
        {
            // Arrange
            TestHelper.ProductService.SearchText = "";
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();
            TestHelper.ProductService.sortColumn = "Title";
            TestHelper.ProductService.sortAsc = false;

            // Get initial products
            var products = TestHelper.ProductService.GetAllData();

            // Act
            var result = TestHelper.ProductService.SortData(products);

            // Get first and last products to compare title ordering
            var firstProduct = result.First();
            var lastProduct = result.Last();

            // Assert
            Assert.AreEqual(true, string.Compare(firstProduct.Title, lastProduct.Title) > 0);
        }
        #endregion SortData

        #region SearchData
        /// <summary>
        /// Validates that an empty search will retrieve all products.
        /// </summary>
        [Test]
        public void SearchData_Valid_EmptySearchText_Should_Return_AllProducts()
        {
            // Arrange
            TestHelper.ProductService.SearchText = "";
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();

            // Get initial products count
            var countOriginal = TestHelper.ProductService.GetAllData().Count();

            // Act
            var result = TestHelper.ProductService.SearchData();

            // Assert
            Assert.AreEqual(countOriginal, result.Count());
        }

        /// <summary>
        /// Validates that a valid search will filter products by title appropriately.
        /// </summary>
        [Test]
        public void SearchData_Valid_SearchText_Should_Return_FilteredProducts()
        {
            // Arrange
            TestHelper.ProductService.SearchText = "Curry";
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();

            // Get the first data item and products count
            var lowerCaseSearch = TestHelper.ProductService.SearchText.ToLower();
            var products = TestHelper.ProductService.GetAllData().Where(
                    x => x.Title.Contains(lowerCaseSearch));

            // Act
            var result = TestHelper.ProductService.SearchData();

            // Assert
            Assert.AreEqual(products.Count(), result.Count());
        }
        #endregion SearchData

        #region SearchIngredients
        /// <summary>
        /// Validates that an empty ingredient search will retrieve all products.
        /// </summary>
        [Test]
        public void SearchIngredients_Valid_EmptySearchText_Should_Return_AllProducts()
        {
            // Arrange
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();
            TestHelper.ProductService.SearchText = "";

            // Get initial products count
            var countOriginal = TestHelper.ProductService.GetAllData().Count();

            // Act
            var result = TestHelper.ProductService.SearchIngredients();

            // Assert
            Assert.AreEqual(countOriginal, result.Count());
        }

        /// <summary>
        /// Validates that a valid search will filter products by ingredient appropriately.
        /// </summary>
        [Test]
        public void SearchIngredients_Valid_IngredientNumber_Should_Return_FilteredProducts()
        {
            // Arrange
            TestHelper.ProductService.IngredientNumbers = new int[] {1};
            TestHelper.ProductService.SearchText = "";

            // Get the products count for the specified ingredient filter
            var products = TestHelper.ProductService.GetAllData().Where(
                    x => x.Ingredients.Where(x => x.Value > 0).Select(x => x.Key).ToList().Contains(1));

            // Act
            var result = TestHelper.ProductService.SearchIngredients();

            // Assert
            Assert.AreEqual(products.Count(), result.Count());
        }
        #endregion SearchIngredients

        #region SearchProductType
        /// <summary>
        /// Validates that an empty producttype search will retrieve all products.
        /// </summary>
        [Test]
        public void SearchProductType_Valid_EmptySearchText_Should_Return_AllProducts()
        {
            // Arrange
            TestHelper.ProductService.ProductTypeNumbers = Enumerable.Empty<int>();
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();
            TestHelper.ProductService.SearchText = "";

            // Get initial products count
            var countOriginal = TestHelper.ProductService.GetAllData().Count();

            // Act
            var result = TestHelper.ProductService.SearchIngredients();

            // Assert
            Assert.AreEqual(countOriginal, result.Count());
        }

        /// <summary>
        /// Validates that a valid search will filter products by ingredient appropriately.
        /// </summary>
        [Test]
        public void SearchProductType_Valid_ProductTypeNumber_Should_Return_FilteredProducts()
        {
            // Arrange
            TestHelper.ProductService.ProductTypeNumbers = new int[] { 1 };
            TestHelper.ProductService.SearchText = "";

            // Get the products count for the specified ingredient filter
            var products = TestHelper.ProductService.GetAllData().Where(
                    x => x.ProductType.Contains(1));

            // Act
            var result = TestHelper.ProductService.SearchProductType();

            // Assert
            Assert.AreEqual(products.Count(), result.Count());
        }
        #endregion

        #region UpdateData
        /// <summary>
        /// Validates that updating a product with valid values will result in a modification
        /// of its database record.
        /// </summary>
        [Test]
        public void UpdateData_Valid_Input_Should_Return_Equal_Result()
        {
            // Arrange
            var originalProduct = TestHelper.ProductService.GetAllData().First();

            // Act
            var testData = TestHelper.ProductService.UpdateData(originalProduct);

            // Assert
            Assert.AreEqual(testData.Id, originalProduct.Id);
        }

        /// <summary>
        /// Validates that updating a product with invalid values will result in no modification
        /// and will return null.
        /// </summary>
        [Test]
        public void UpdateData_Invalid_Input_Null_Should_Return_Null()
        {
            //Arrange
            var testData = TestHelper.ProductService.CreateData();
            var testInput = TestHelper.ProductService.CreateData();
            testInput.Id = null;

            //Act
            testData = TestHelper.ProductService.UpdateData(testInput);

            //Assert            
            Assert.AreEqual(null, testData);
        }
        #endregion UpdateData

        #region AddData
        /// <summary>
        /// Validates that adding with a valid product will add the record to the encapsulated
        /// records.
        /// </summary>
        [Test]
        public void AddData_Valid_Input_Null_Should_Return_Input()
        {
            // Arrange
            var input = new ProductModel()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Easy Smoked Sausage Skillet",
                Description = "Looking for a quick sausage recipe for dinner? You\u0027ll have dinner on the table in no time with this one-skillet sausage and veggie dish served over rice.",
                Url = "https://www.allrecipes.com/recipe/245932/easy-smoked-sausage-skillet/",
                Image = "https://www.allrecipes.com/thmb/jyUz3X58_Qi2j9qlGR2pOysRItU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/3485679-cc7318539dd9438aab7d0ac79be51612.jpg",
                Instructions = "",
            };

            // Get all records and store the initial products count
            var data = TestHelper.ProductService.GetAllData();
            var countInit = data.Count();

            // Act
            var addedRecord = TestHelper.ProductService.AddData(input);

            // Retrieve the modified count
            var countMod = TestHelper.ProductService.GetAllData().Count();

            // Reset
            TestHelper.ProductService.DeleteData(input.Id);

            // Assert
            Assert.AreEqual(countInit + 1, countMod);
            Assert.AreEqual(input.Id, addedRecord.Id);
        }
        #endregion AddData

        #region Paginate
        /// <summary>
        /// Validates that paginating product records with the value of 0 will retrieve the first
        /// page in the subsetted records.
        /// </summary>
        [Test]
        public void Paginate_Valid_PageEqualTo0_Should_Return_FirstPages()
        {
            //Arrange

            //Act
            TestHelper.ProductService.page = 0;
            var results = TestHelper.ProductService.Paginate();

            //Assert            
            Assert.AreEqual(5, results.Count());
        }

        /// <summary>
        /// Validates that paginating product records with the value of greater than 0 will 
        /// retrieve the correct page in the subsetted records.
        /// </summary>
        [Test]
        public void Paginate_Valid_PageGreaterThan0_Should_Return_SubsequentsPages()
        {
            //Arrange

            //Act
            TestHelper.ProductService.page = 1;
            var results = TestHelper.ProductService.Paginate();

            //Assert            
            Assert.AreEqual(5, results.Count());
        }
        #endregion Paginate

        #region GetMaxPages
        /// <summary>
        /// Validates that retrieving the maximum number of pages returns the expected results.
        /// </summary>
        [Test]
        public void GetMaxPages_Valid_Should_Return_MaximumNumberOfPages()
        {
            // Arrange

            // Retrieve expected maximum number of pages
            var recordCount = TestHelper.ProductService.GetAllData().Count();
            var pageSize = TestHelper.ProductService.pageSize;
            var maxPages = (int)Math.Ceiling(recordCount / (double)pageSize);

            // Act
            var results = TestHelper.ProductService.GetMaxPages();

            // Assert            
            Assert.AreEqual(maxPages, results);
        }
        #endregion GetMaxPages

        #region SetPageSize
        /// <summary>
        /// Validates the assigning of a new page size has the expected behavior.
        /// </summary>
        [Test]
        public void SetPageSize_Valid_Should_AssignNewPageSize()
        {
            // Arrange

            // Retrieve initial page size
            var initialPageSize = TestHelper.ProductService.pageSize;

            // Act
            TestHelper.ProductService.pageSize = -1;
            var results = TestHelper.ProductService.pageSize;

            // Reset
            TestHelper.ProductService.pageSize = 5;

            // Assert            
            Assert.AreNotEqual(initialPageSize, results);
        }
        #endregion SetPageSize

        #region SetSortColumn
        /// <summary>
        /// Validates the assigning of a new sort column has the expected behavior.
        /// </summary>
        [Test]
        public void SetSortColumn_Valid_Should_AssignNewSortColumn()
        {
            // Arrange

            // Retrieve initial sort column
            var initialSortCol = TestHelper.ProductService.sortColumn;

            // Act
            TestHelper.ProductService.sortColumn = "TimeStamp";
            var results = TestHelper.ProductService.sortColumn;

            // Reset
            TestHelper.ProductService.sortColumn = "Title";

            // Assert            
            Assert.AreNotEqual(initialSortCol, results);
        }
        #endregion SetSortColumn

        #region SetSortAsc
        /// <summary>
        /// Validates the assigning of a new sort direction has the expected behavior.
        /// </summary>
        [Test]
        public void SetSortDir_Valid_Should_AssignNewSortColumn()
        {
            // Arrange

            // Retrieve initial sort column
            var initialSortDir = TestHelper.ProductService.sortAsc;

            // Act
            TestHelper.ProductService.sortAsc = true;
            var results = TestHelper.ProductService.sortAsc;

            // Reset
            TestHelper.ProductService.sortAsc = false;

            // Assert            
            Assert.AreNotEqual(initialSortDir, results);
        }
        #endregion SetSortAsc
    }
}