using NUnit.Framework;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Models.IngredientType
{
    /// <summary>
    /// Tests for the IngredientTypeEnum class.
    /// </summary>
    public class IngredientTypeEnumTests
    {
        #region DisplayName
        /// <summary>
        /// Validates that the Undefied enum yields an empty string for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Undefined_Should_Return_EmptyString()
        {
            // Arrange
            var result = IngredientTypeEnum.Undefined;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Beef enum yields the string "Beef" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Beef_Should_Return_Beef()
        {
            // Arrange
            var result = IngredientTypeEnum.Beef;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Beef", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Butter enum yields the string "Butter" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Butter_Should_Return_Butter()
        {
            // Arrange
            var result = IngredientTypeEnum.Butter;

            // Act

            // Reset
            
            // Assert
            Assert.AreEqual("Butter", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Chicken enum yields the string "Chicken" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Chicken_Should_Return_Butter()
        {
            // Arrange
            var result = IngredientTypeEnum.Chicken;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Chicken", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Egg enum yields the string "Egg" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Egg_Should_Return_Egg()
        {
            // Arrange
            var result = IngredientTypeEnum.Egg;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Egg", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Flour enum yields the string "Flour" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Flour_Should_Return_Flour()
        {
            // Arrange
            var result = IngredientTypeEnum.Flour;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Flour", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Garlic enum yields the string "Garlic" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Garlic_Should_Return_Garlic()
        {
            // Arrange
            var result = IngredientTypeEnum.Garlic;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Garlic", result.DisplayName());
        }

        /// <summary>
        /// Validates that the OliveOil enum yields the string "Olive Oil" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_OliveOil_Should_Return_OliveOil()
        {
            // Arrange
            var result = IngredientTypeEnum.OliveOil;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Olive Oil", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Onion enum yields the string "Onion" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Onion_Should_Return_Onion()
        {
            // Arrange
            var result = IngredientTypeEnum.Onion;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Onion", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Pepper enum yields the string "Pepper" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Pepper_Should_Return_Pepper()
        {
            // Arrange
            var result = IngredientTypeEnum.Pepper;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Pepper", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Pork enum yields the string "Pork" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Pork_Should_Return_Pork()
        {
            // Arrange
            var result = IngredientTypeEnum.Pork;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Pork", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Potato enum yields the string "Potato" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Potato_Should_Return_Potato()
        {
            // Arrange
            var result = IngredientTypeEnum.Potato;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Potato", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Rice enum yields the string "Rice" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Rice_Should_Return_Rice()
        {
            // Arrange
            var result = IngredientTypeEnum.Rice;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Rice", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Sugar enum yields the string "Sugar" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Sugar_Should_Return_Sugar()
        {
            // Arrange
            var result = IngredientTypeEnum.Sugar;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Sugar", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Salt enum yields the string "Salt" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Salt_Should_Return_Salt()
        {
            // Arrange
            var result = IngredientTypeEnum.Salt;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Salt", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Water enum yields the string "Water" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Water_Should_Return_Water()
        {
            // Arrange
            var result = IngredientTypeEnum.Water;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Water", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Fish enum yields the string "Fish" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Fish_Should_Return_Fish()
        {
            // Arrange
            var result = IngredientTypeEnum.Fish;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Fish", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Turkey enum yields the string "Turkey" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Turkey_Should_Return_Turkey()
        {
            // Arrange
            var result = IngredientTypeEnum.Turkey;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Turkey", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Broccoli enum yields the string "Broccoli" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Broccoli_Should_Return_Broccoli()
        {
            // Arrange
            var result = IngredientTypeEnum.Broccoli;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Broccoli", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Carrot enum yields the string "Carrot" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Carrot_Should_Return_Carrot()
        {
            // Arrange
            var result = IngredientTypeEnum.Carrot;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Carrot", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Tomato enum yields the string "Tomato" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Tomato_Should_Return_Tomato()
        {
            // Arrange
            var result = IngredientTypeEnum.Tomato;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Tomato", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Legumes enum yields the string "Legumes" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Legumes_Should_Return_Legumes()
        {
            // Arrange
            var result = IngredientTypeEnum.Legumes;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Legumes", result.DisplayName());
        }

        /// <summary>
        /// Validates that the Lamb enum yields the string "Lamb" for its display name.
        /// </summary>
        [Test]
        public void DisplayName_Lamb_Should_Return_Lamb()
        {
            // Arrange
            var result = IngredientTypeEnum.Lamb;

            // Act

            // Reset

            // Assert
            Assert.AreEqual("Lamb", result.DisplayName());
        }
        #endregion DisplayName
    }
}