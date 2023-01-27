using NUnit.Framework;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Models.ProductType
{
    /// <summary>
    /// Tests for the ProductTypeEnum class.
    /// </summary>
    public class ProductTypeEnumTests
    {
        #region DisplayName
        /// <summary>
        /// Validates that the Undefied enum yields an empty string for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Undefined_Should_Return_EmptyString()
        {
            // Arrange
            var productType = ProductTypeEnum.Undefined;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the Vegetarian enum yields the string "Vegetarian" for its display
        /// name.
        /// </summary>
        [Test]
        public void DisplayName_Vegetarian_Should_Return_Vegetarian()
        {
            // Arrange
            var productType = ProductTypeEnum.Vegetarian;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Vegetarian", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the Vegan enum yields the string "Vegan" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Vegan_Should_Return_Vegan()
        {
            // Arrange
            var productType = ProductTypeEnum.Vegan;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Vegan", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the DairyFree enum yields the string "Dairy-Free" for its display 
        /// name.
        /// </summary>
        [Test]
        public void DisplayName_DairyFree_Should_Return_DairyFree()
        {
            // Arrange
            var productType = ProductTypeEnum.DairyFree;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Dairy-Free", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the GlutenFree enum yields the string "Gluten-Free" for its display
        /// name.
        /// </summary>
        [Test]
        public void DisplayName_GlutenFree_Should_Return_GlutenFree()
        {
            // Arrange
            var productType = ProductTypeEnum.GlutenFree;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Gluten-Free", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the LowCarb enum yields the string "Low Carb" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_LowCarb_Should_Return_LowCarb()
        {
            // Arrange
            var productType = ProductTypeEnum.LowCarb;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Low-Carb", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the Keto enum yields the string "Keto" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Keto_Should_Return_Keto()
        {
            // Arrange
            var productType = ProductTypeEnum.Keto;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Keto", productType.DisplayName());
        }

        /// <summary>
        /// Validates that the LowFat enum yields the string "Low-Fat" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_LowFat_Should_Return_LowFat()
        {
            // Arrange
            var productType = ProductTypeEnum.LowFat;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Low-Fat", productType.DisplayName());
        }
        #endregion DisplayName
    }
}