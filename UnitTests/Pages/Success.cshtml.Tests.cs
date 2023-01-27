using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Success
{
    /// <summarsy>
    /// Tests for the Success page.
    /// </summary>
    public class SuccessTests
    {
        #region TestSetup
        // The model representing the model of the Success page
        public static SuccessModel pageModel;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new SuccessModel()
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