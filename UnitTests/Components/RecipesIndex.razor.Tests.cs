using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using Bunit;

namespace UnitTests.Components
{
    /// <summary>
    /// Tests for the RecipesIndex razor component.
    /// </summary>
    public class RecipesIndexTests : BunitTestContext
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

        #region RecipesIndex
        /// <summary>
        /// Validates that the initialized RecipesIndex component renders content.
        /// </summary>
        [Test]
        public void RecipesIndex_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Get the table rows returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("Recipes"));
        }
        #endregion RecipesIndex

        #region NextPage
        /// <summary>
        /// Validates that clicking the "next-button" will result in the retrieval of the next
        /// paginated page.
        /// </summary>
        [Test]
        public void NextPage_Button_Click_Valid_Should_Return_NextPaginatedPage()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Invoke page navigation
            page.Find("#next-button").Click();
            var result = page.Find("#page-num-label");

            // Reset
            TestHelper.ProductService.page = 1;

            // Assert
            result.MarkupMatches("<b id=\"page-num-label\">2</b>");
        }
        #endregion NextPage

        #region PrevPage
        /// <summary>
        /// Validates that clicking the "prev-button" will result in the retrieval of the 
        /// same paginated page if the starting page is 1.
        /// </summary>
        [Test]
        public void PrevPage_Button_Click_Invalid_Should_Return_SamePaginatedPage()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Invoke page navigation
            page.Find("#prev-button").Click();
            var result = page.Find("#page-num-label");

            // Reset
            TestHelper.ProductService.page = 1;

            // Assert
            result.MarkupMatches("<b id=\"page-num-label\">1</b>");
        }

        /// <summary>
        /// Validates that clicking the "prev-button" will result in the retrieval of the 
        /// previous paginated page if the starting page is greater than 1.
        /// </summary>
        [Test]
        public void PrevPage_Button_Click_Valid_Should_Return_PrevPaginatedPage()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 2; 

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Invoke page navigation
            page.Find("#prev-button").Click();
            var result = page.Find("#page-num-label");

            // Reset
            TestHelper.ProductService.page = 1;

            // Assert
            result.MarkupMatches("<b id=\"page-num-label\">1</b>");
        }
        #endregion PrevPage

        #region SelectSortColumn
        /// <summary>
        /// Validates that changing of the sort by selector will result in the retrieval of the 
        /// paginated page sorted by that specified column.
        /// </summary>
        [Test]
        public void SelectSortColumn_SelectController_Change_Valid_Should_Return_SortedPaginatedPage()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.pageSize = 5;
            TestHelper.ProductService.sortAsc = false;
            TestHelper.ProductService.sortColumn = "Title";

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Invoke page navigation
            page.Find("#sort-by-selector").Change("TimeStamp");
            var result = page.Markup;

            // Reset
            TestHelper.ProductService.sortColumn = "Title";

            // Assert
            Assert.AreEqual(true, result.Contains("Blueberry Smoothie"));
        }
        #endregion SelectSortColumn

        #region SelectSortDir
        /// <summary>
        /// Validates that changing of the sort direction selector will result in the retrieval of the 
        /// paginated page sorted by that specified direction.
        /// </summary>
        [Test]
        public void SelectSortDir_SelectController_Change_Valid_Should_Return_SortedPaginatedPage()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.sortAsc = false;
            TestHelper.ProductService.sortColumn = "Title";

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Invoke page navigation
            page.Find("#sort-dir-selector").Change("True");
            var result = page.Markup;

            // Reset
            TestHelper.ProductService.sortAsc = false;

            // Assert
            Assert.AreEqual(true, result.Contains("Blueberry Smoothie"));
        }
        #endregion SelectSortDir

        #region SelectPageSize
        /// <summary>
        /// Validates that changing of the page size selector will result in the retrieval of the 
        /// paginated page with the specified size.
        /// </summary>
        [Test]
        public void SelectPageSize_SelectController_Change_Valid_Should_Return_PaginatedPage()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.pageSize = 5;
            TestHelper.ProductService.sortAsc = false;
            TestHelper.ProductService.sortColumn = "Title";

            // Act
            var page = RenderComponent<RecipesIndex>();

            // Invoke page navigation
            page.Find("#page-size-selector").Change("10");
            var result = page.Markup;

            // Reset
            TestHelper.ProductService.pageSize = 5;

            // Assert
            Assert.AreEqual(true, result.Contains("Classic Macaroni Salad"));
        }
        #endregion SelectPageSize
    }
}