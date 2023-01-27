using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Home
{
    /// <summary>
    /// Tests for the Home page.
    /// </summary>
    public class HomeTests
    {
        #region TestSetup
        // The model representing the model of the Home page
        public static HomeModel pageModel;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new HomeModel()
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

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnInitialization
    }
}