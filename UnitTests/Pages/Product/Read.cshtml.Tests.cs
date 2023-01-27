using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Product
{
    /// <summary>
    /// Tests for the CRUDi Read page.
    /// </summary>
    public class ReadTests
    {
        #region TestSetup
        // The model representing the model of the Index page
        public static ReadModel pageModel;

        /// <summary>
        /// Test setup
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ReadModel(TestHelper.ProductService)
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
            Assert.AreEqual(pageModel.product.Id, initProduct.Id);
        }

        /// <summary>
        /// A GET request for an invalid Product should return that Product
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
        #endregion OnGet
    }
}