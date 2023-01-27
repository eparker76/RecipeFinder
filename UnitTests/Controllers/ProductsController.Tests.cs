using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Tests for the ProductsControllers.
    /// </summary>
    public class ProductsControllerTests
    {
        #region TestSetup
        // The controller that controlls UI interactions with the products
        public static ProductsController productsController;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            productsController = new ProductsController(TestHelper.ProductService);
        }
        #endregion TestSetup

        #region OnInitialization
        /// <summary>
        /// Validates that the ProductsController Object is initialized correctly.
        /// </summary>
        [Test]
        public void OnInitialization_Valid_Should_SetAttributes()
        {
            // Arrange
            var testData = productsController.ProductService;
            // Act

            // Assert
            Assert.AreNotEqual(null, testData);
        }
        #endregion OnInitialization

        #region Get
        /// <summary>
        /// Validates that the Get method returns all products.
        /// </summary>
        [Test]
        public void Get_Valid_Should_Return_AllProducts()
        {
            // Arrange

            // Retrieve all epected product records
            var products = TestHelper.ProductService.GetAllData();

            // Act
            var results = productsController.Get();

            // Assert
            Assert.AreEqual(results.Count(), products.Count());
        }
        #endregion Get

        #region Patch
        /// <summary>
        /// Validates that the Patch method adds a new rating to the specified product.
        /// </summary>
        [Test]
        public void Patch_Valid_Should_AssignNewRating()
        {
            // Arrange

            // Get the First data item that has ratings
            var initData = TestHelper.ProductService.GetAllData().Where(
                x => x.Ratings != null).First();
            var initCount = initData.Ratings.Length;

            // Make a rating request for 5 stars
            var ratingReq = new ProductsController.RatingRequest()
            {
                ProductId = initData.Id,
                Rating = 5
            };

            // Act            
            var results = productsController.Patch(ratingReq);

            // Retrieve modified rating count
            var modData = TestHelper.ProductService.GetProductById(initData.Id);
            var modCount = modData.Ratings.Length;

            // Reset
            TestHelper.ProductService.UpdateData(initData);

            // Assert
            Console.WriteLine(results.GetType());
            Assert.True(results is OkResult);
            Assert.AreEqual(initCount + 1, modCount);
        }
        #endregion Patch
    }
}