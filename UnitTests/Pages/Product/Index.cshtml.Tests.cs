using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Product
{
    /// <summary>
    /// Tests for the CRUDi index page.
    /// </summary>
    public class IndexTests
    {
        // The model representing the model of the CRUDi index page
        public static ProductIndexModel pageModel;

        #region TestSetup
        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new ProductIndexModel()
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

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnInitialization
    }
}