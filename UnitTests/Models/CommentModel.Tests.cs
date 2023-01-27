using NUnit.Framework;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Models.Comment
{
    /// <summary>
    /// Tests for the CommentModel class.
    /// </summary>
    public class CommentModelTests
    {
        #region GetId
        /// <summary>
        /// Validates that initialized comment models have unique identifiers.
        /// </summary>
        [Test]
        public void GetId_Should_Return_Unique_Guid()
        {
            // Arrange
            var commentModel1 = new CommentModel();
            var commentModel2 = new CommentModel();

            // Act

            // Reset

            // Assert
            Assert.AreNotEqual(commentModel1.Id, commentModel2.Id);
        }
        #endregion GetId

        #region GetComment
        /// <summary>
        /// Validates that the comment accessor for a comment model object initialized without a 
        /// comment will return null.
        /// </summary>
        [Test]
        public void GetComment_Invalid_Should_Return_Null()
        {
            // Arrange
            var commentModel1 = new CommentModel();

            // Act

            // Reset

            // Assert
            Assert.AreEqual(null, commentModel1.Comment);
        }

        /// <summary>
        /// Validates that the comment accessor for a comment model Object initialized with a 
        /// comment will return the encapsulated comment. 
        /// </summary>
        [Test]
        public void GetComment_Valid_Should_Return_Comment()
        {
            // Arrange
            string mockComment = "mock comment";
            var commentModel1 = new CommentModel()
            {
                Comment = mockComment
            };

            // Act

            // Reset

            // Assert
            Assert.AreEqual(mockComment, commentModel1.Comment);
        }
        #endregion GetComment

        #region SetId
        /// <summary>
        /// Validates that the setter for the identifiers attribute in a comment model object will
        /// assign a new value.
        /// </summary>
        [Test]
        public void SetId_Valid_Should_Assign_New_IdValue()
        {
            // Arrange
            var commentModel1 = new CommentModel();
            var initialGuid = commentModel1.Id;
            string newId = "id#4";

            // Act
            commentModel1.Id = newId;

            // Reset

            // Assert
            Assert.AreNotEqual(initialGuid, commentModel1.Id);
            Assert.AreEqual(newId, commentModel1.Id);
        }
        #endregion SetId

        #region SetComment
        /// <summary>
        /// Validates that the setter for the comment attribute in a comment model object will
        /// assign a new value.
        /// </summary>
        [Test]
        public void SetComment_Valid_Should_Assign_New_CommentValue()
        {
            // Arrange
            var commentModel1 = new CommentModel();
            var initialComment = commentModel1.Comment;
            string newComment = "this is a new comment";

            // Act
            commentModel1.Comment = newComment;

            // Reset

            // Assert
            Assert.AreNotEqual(initialComment, commentModel1.Comment);
            Assert.AreEqual(newComment, commentModel1.Comment);
        }
        #endregion SetComment
    }
}