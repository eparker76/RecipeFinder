using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using System.Linq;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product
{
    /// <summary>
    /// Tests for the CRUDi Create page.
    /// </summary>
    public class CreateTests
    {
        #region TestSetup
        // The model representing the model of the Create page
        public static CreateModel pageModel;

        /// <summary>
        /// Setup for Unit Testing
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateModel(TestHelper.ProductService)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region Initialize
        /// <summary>
        /// Verifies that Product can use Get 
        /// </summary>
        [Test]
        public void Product_Should_Be_Gettable()
        {
            //Arrange
            string data = "Data";

            //Act
            pageModel.Product.Title = data;

            //Assert
            Assert.AreEqual(data, pageModel.Product.Title);
        }
        /// <summary>
        /// Verifies that Product can use Set 
        /// </summary>
        [Test]
        public void Product_Should_Be_Settable()
        {
            //Arrange
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Test Title",
                Description = "Test Description",
                Url = "Test URL",
                Image = "Test Image",
                Instructions = "Test Instructions",
            };

            //Act
            pageModel.Product = data;

            //Assert
            Assert.AreEqual(data, pageModel.Product);
        }
        #endregion Initialize

        #region OnGet
        /// <summary>
        /// A valid GET request should return True 
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Have_Valid_ModelState()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// An onPost with an Invalid Product should return to the Page
        /// </summary>
        [Test]
        public void OnPost_Invalid_Product_Should_Have_Valid_ModelState()
        {
            //Arrange
            string title = "Enter Title";

            //Act
            pageModel.Product.Title = title;
            pageModel.OnPost();

            //Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        /// <summary>
        /// An onPost with a Valid Product should add data and return to index
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_Should_AddData_And_Return_To_Index()
        {
            //Arrange
            string title = "My New Title";

            //Act
            pageModel.Product.Title = title;
            pageModel.OnPost();
            var result = TestHelper.ProductService.GetAllData().Where(x => x.Title == title).First();

            //Assert
            
            Assert.AreEqual(result.ToString(), pageModel.Product.ToString());
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnPost

        #region validProduct
        /// <summary>
        /// A product with "Enter Title" should not count as valid 
        /// </summary>
        [Test]
        public void Default_Title_Should_Return_False()
        {
            //Arrange
            string data = "Enter Title";

            //Act
            pageModel.Product.Title = data;

            //Assert
            Assert.AreEqual(false, pageModel.validProduct());
        }

        /// <summary>
        /// A product with "" for a title should return false 
        /// </summary>
        [Test]
        public void No_Title_Should_Return_False()
        {
            //Arrange
            string data = "";

            //Act
            pageModel.Product.Title = data;

            //Assert
            Assert.AreEqual(false, pageModel.validProduct());
        }
        #endregion validProduct
    }
}