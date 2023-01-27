using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Tests for the Error page.
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup
        // The model representing the model of the Error page
        public static ErrorModel pageModel;

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }
        #endregion TestSetup

        #region SetErrorMessage
        /// <summary>
        /// Validates that the setter for the encapsulated error message has the expected behavior
        /// when assigning a new error message.
        /// </summary>
        [Test]
        public void SetErrorMessage_Valid_Should_AssignNewErrorMessage()
        {
            // Arrange

            // Act
            pageModel.ErrorMessage = "A error message";
            var result = pageModel.ErrorMessage;

            // Reset
            pageModel.ErrorMessage = null;

            // Assert
            Assert.AreEqual(result, "A error message");
        }
        #endregion SetErrorMessage

        #region OnGet
        /// <summary>
        /// Validates that the OnGet method invocation with an initialized activity will assign
        /// the request identifier as the activity identifier.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange

            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet(null);

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// Validates that the OnGet method invocation without an initialized activity will assign
        /// the request identifier as trace.
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet(null);

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }

        /// <summary>
        /// Validates that the OnGet method invocation with a specified error message 
        /// assigns a new value to the encapsulated error message.
        /// </summary>
        [Test]
        public void OnGet_Valid_ErrorMessage_Should_AssignNewErrorMessage()
        {
            // Arrange

            // Act
            pageModel.OnGet("Another error message");
            var result = pageModel.ErrorMessage;

            // Reset
            pageModel.ErrorMessage = null;

            // Assert
            Assert.AreEqual(result, "Another error message");
        }
        #endregion OnGet
    }
}