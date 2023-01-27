using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages.Product;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UnitTests.Pages.Product
{
    /// <summary>
    /// Tests for the CRUDi update page.
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup
        // The model representing the model of the Update page
        public static UpdateModel pageModel;

        /// <summary>
        /// Setup for Unit Testing
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.ProductService)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// A GET request for a invalid Product with an identifier of "" should redirect to the 
        /// index page.
        /// </summary>
        [Test]
        public void OnGet_InValid_ProductEmptyStr_Should_Return_IndexPage()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("") as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// A GET request for a invalid Product with an identifier of null should redirect to the 
        /// index page.
        /// </summary>
        [Test]
        public void OnGet_InValid_ProductNull_Should_Return_IndexPage()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet(null) as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// A GET request for a valid Product should return that Product
        /// </summary>
        [Test]
        public void OnGet_Valid_Product_Should_Return_Product()
        {
            // Arrange

            // Get the first product with ratings and its respective ratings count
            var initProduct = TestHelper.ProductService.GetAllData().First();

            // Act
            pageModel.OnGet(initProduct.Id);

            // Assert
            Assert.AreEqual(pageModel.Product.Id, initProduct.Id);
        }

        /// <summary>
        /// A GET request for an invalid Product should return the Index page.
        /// </summary>
        [Test]
        public void OnGet_InValid_ProductID_NotFound_Should_Return_IndexPage()
        {
            // Arrange

            // Act
            var result = pageModel.OnGet("-1") as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, result.PageName.Contains("Error"));
        }

        /// <summary>
        /// A GET request for an valid Product without initial ingredients should return that Product
        /// and add ingredient records.
        /// </summary>
        [Test]
        public void OnGet_Valid_ProductID_NoIngredients_Should_Return_RecipeWithIngredients()
        {
            // Arrange

            // Add product with empty 
            var product = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Example",
                Description = "Example description",
                Url = "https://google.com",
                Image = "",
                Instructions = ""
            };
            TestHelper.ProductService.AddData(product);

            // Act
            pageModel.OnGet(product.Id);
            var result = pageModel.Product.Ingredients.Count();

            // Reset
            TestHelper.ProductService.DeleteData(product.Id);

            // Assert
            Assert.AreEqual(true, result > 0);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// A POST request for a valid existing Product should return True 
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Return_RecipesTablePage()
        {
            // Arrange
            pageModel.ModelState.Clear();
            var initProduct = TestHelper.ProductService.GetAllData().First();
            pageModel.Product = new ProductModel
            {
                Id = initProduct.Id,
                Maker = "Reddit User",
                Image = "IMG",
                Url = "URL",
                Title = "Pho",
                Description = "Pho is yummy."
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Success"));
        }

        /// <summary>
        /// A POST request for an invalid model should return a page with an invalid model state
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Should_Return_Page()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError("mock error", "mock error message");

            // Act
            pageModel.OnPost();

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPost
    }
}