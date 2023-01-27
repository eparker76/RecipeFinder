using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using MudBlazor.Services;
using Bunit;
using System.Linq;

namespace UnitTests.Components
{
    /// <summary>
    /// Tests for the ProductList razor component.
    /// </summary>
    public class ProductListTests : BunitTestContext
    {
        #region TestSetup
        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            Services.AddMudServices();
        }

        #endregion TestSetup

        #region ProductList
        /// <summary>
        /// Validates that the initialized ProductList component renders content.
        /// </summary>
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.SearchText = "";
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();
            TestHelper.ProductService.ProductTypeNumbers = Enumerable.Empty<int>();

            // Act
            var page = RenderComponent<ProductList>();

            // Get the Cards returned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("<b id=\"page-num-label\">1</b>"));
        }
        #endregion ProductList

        #region IngredientSelect
        /// <summary>
        /// Validates that clicking the "ingredient-option-1" MudChip will result in the retrieval 
        /// of filtered recipes with that ingredient in the paginated page.
        /// </summary>
        [Test]
        public void IngredientSelect_Valid_Should_Return_FilteredPaginatedPage()
        {
            // Arrange
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 3;
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();

            // Act
            var page = RenderComponent<ProductList>();

            // Invoke page navigation
            page.Find("#ingredient-option-1").Click();
            var result = page.Find("#page-num-label");

            // Reset
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();

            // Assert
            result.MarkupMatches("<b id=\"page-num-label\">1</b>");
        }
        #endregion IngredientSelect

        #region ProductTypeSelect
        /// <summary>
        /// Validates that clicking the "recipe-type-option-option-1" MudChip will result in the 
        /// retrieval of filtered recipes with that ingredient in the paginated page.
        /// </summary>
        [Test]
        public void ProductTypeSelect_Valid_Should_Return_FilteredPaginatedPage()
        {
            // Arrange
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.ProductTypeNumbers = Enumerable.Empty<int>();

            // Act
            var page = RenderComponent<ProductList>();

            // Invoke page navigation
            page.Find("#recipe-type-option-1").Click();
            var result = page.Find("#page-num-label");

            // Reset
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.ProductTypeNumbers = Enumerable.Empty<int>();

            // Assert
            result.MarkupMatches("<b id=\"page-num-label\">1</b>");
        }
        #endregion ProductTypeSelect

        #region NextPage
        /// <summary>
        /// Validates that clicking the "next-button" will result in the retrieval of the next
        /// paginated page.
        /// </summary>
        [Test]
        public void NextPage_Button_Click_Valid_Should_Return_NextPaginatedPage()
        {
            // Arrange
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

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
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

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
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 2;

            // Act
            var page = RenderComponent<ProductList>();

            // Invoke page navigation
            page.Find("#prev-button").Click();
            var result = page.Find("#page-num-label");

            // Reset
            TestHelper.ProductService.page = 1;

            // Assert
            result.MarkupMatches("<b id=\"page-num-label\">1</b>");
        }
        #endregion PrevPage

        #region Enter
        /// <summary>
        /// Validates that searching will result in the filteration of data.
        /// </summary>
        [Test]
        public void Enter_Keydown_Valid_Should_Return_PrevPaginatedPage()
        {
            // Arrange
            TestContext.JSInterop.Mode = JSRuntimeMode.Loose;
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            TestHelper.ProductService.page = 1;
            TestHelper.ProductService.IngredientNumbers = Enumerable.Empty<int>();

            // Act
            var page = RenderComponent<ProductList>();
            TestHelper.ProductService.SearchText = "curry";

            // Invoke page navigation
            page.Find("#search-input").KeyDown(Key.Down);
            var result = page.Markup;

            // Reset
            TestHelper.ProductService.SearchText = "";

            // Assert
            Assert.AreEqual(true, result.Contains("<b id=\"page-num-label\">1</b>"));
        }
        #endregion Enter
    }
}