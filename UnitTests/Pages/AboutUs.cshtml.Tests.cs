using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Tests for the AboutUs page.
    /// </summary>
    public class AboutUsTests
    {
        #region TestSetup
        // The model representing the model of the AboutUs page
        public static AboutUsModel pageModel;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<AboutUsModel>>();

            pageModel = new AboutUsModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region OnInitialization
        /// <summary>
        /// Validates that the initialized page has a valid state.
        /// </summary>
        [Test]
        public void OnInitialization_Valid_Should_Have_Valid_ModelState()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnInitialization
    }
}