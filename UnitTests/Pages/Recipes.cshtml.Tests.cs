using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Pages.Recipes
{
    /// <summary>
    /// Tests for the Recipes page.
    /// </summary>
    public class RecipesTests
    {
        #region TestSetup
        // The model representing the model of the Recipe page
        public static RecipeModel pageModel;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new RecipeModel(TestHelper.ProductService)
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

        #region GetGurrentRating
        /// <summary>
        /// Validates that the retrieval of ratings for a product with multiple ratings will update
        /// encapsulated attributes of the page appropriately.
        /// </summary>
        [Test]
        public void GetGurrentRating_Valid_ProductWithMultipleRatings_Should_SetCounts()
        {
            // Arrange

            // Get the first product with ratings and its respective ratings count
            var initProduct = TestHelper.ProductService.GetAllData().Where(
                x => x.Ratings != null && x.Ratings.Count() > 1).FirstOrDefault();
            var ratingsCount = initProduct.Ratings.Count();

            // Act
            pageModel.OnGet(initProduct.Id);

            // Assert
            Assert.AreEqual(pageModel.voteLabel, "Votes");
            Assert.AreEqual(pageModel.voteCount, ratingsCount);
            Assert.AreEqual(pageModel.currentRating, initProduct.Ratings.Sum() / ratingsCount);
        }

        /// <summary>
        /// Validates that the retrieval of ratings for a product with a single rating will update
        /// encapsulated attributes of the page appropriately.
        /// </summary>
        [Test]
        public void GetGurrentRating_Valid_ProductWithSingleRating_Should_SetCounts()
        {
            // Arrange

            // Get the first product with a single rating
            var initProduct = TestHelper.ProductService.GetAllData().Where(
                x => x.Ratings != null && x.Ratings.Count() == 1).FirstOrDefault();
            var ratingsCount = initProduct.Ratings.Count();

            // Act
            pageModel.OnGet(initProduct.Id);

            // Assert
            Assert.AreEqual(pageModel.voteLabel, "Vote");
            Assert.AreEqual(pageModel.voteCount, ratingsCount);
            Assert.AreEqual(pageModel.currentRating, initProduct.Ratings.Sum() / ratingsCount);
        }

        /// <summary>
        /// Validates that the retrieval of ratings for a product with no ratings will update
        /// encapsulated attributes of the page appropriately.
        /// </summary>
        [Test]
        public void GetGurrentRating_ProductWithNoRating_Should_SetCounts()
        {
            // Arrange

            // Get the first product with a single rating
            var initProduct = TestHelper.ProductService.GetAllData().Where(
                x => x.Ratings == null).FirstOrDefault();

            // Act
            pageModel.OnGet(initProduct.Id);

            // Assert
            Assert.AreEqual(pageModel.voteCount, 0);
            Assert.AreEqual(pageModel.currentRating, 0);
        }
        #endregion GetGurrentRating

        #region OnPost
        /// <summary>
        /// Validates that valid ratings should register new ratings.
        /// </summary>
        [Test]
        public void OnPost_Valid_Rating_Should_AddNewRating()
        {
            // Arrange

            // Get the first product with ratings and its respective ratings count
            var initProduct = TestHelper.ProductService.GetAllData().FirstOrDefault(
                x => x.Ratings != null);
            var initRatingsCount = initProduct.Ratings.Length;

            // Act
            pageModel.rating = 5;
            pageModel.OnPost(initProduct.Id);

            // Get the modified product ratings count
            var modProduct = TestHelper.ProductService.GetAllData().FirstOrDefault(
                x => x.Id == initProduct.Id);

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(initRatingsCount + 1, modProduct.Ratings.Length);
        }
        #endregion OnPost
    }
}