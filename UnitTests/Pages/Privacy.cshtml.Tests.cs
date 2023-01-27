using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Tests for the Privacy page.
    /// </summary>
    public class PrivacyTests
    {
        #region TestSetup
        // The model representing the model of the Privacy page
        public static PrivacyModel pageModel;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            pageModel = new PrivacyModel(MockLoggerDirect)
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