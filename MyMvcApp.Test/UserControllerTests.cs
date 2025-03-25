using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;
using System.Collections.Generic;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        private UserController _controller;

        public UserControllerTests()
        {
            // Initialize the controller and reset the static user list
            UserController.userlist = new List<User>();
            _controller = new UserController();
        }

        [Fact]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result.Model);
            Assert.Single((List<User>)result.Model);
        }

        [Fact]
        public void Details_UserExists_ReturnsViewWithUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Details_UserDoesNotExist_ReturnsNotFound()
        {
            // Act
            var result = _controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ValidUser_AddsUserAndRedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = _controller.Create(user) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(UserController.userlist);
            Assert.Equal(user, UserController.userlist[0]);
        }

        [Fact]
        public void Create_InvalidModel_ReturnsViewWithUser()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");
            var user = new User { Id = 1, Email = "john@example.com" };

            // Act
            var result = _controller.Create(user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Edit_UserExists_ReturnsViewWithUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = _controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Edit_UserDoesNotExist_ReturnsNotFound()
        {
            // Act
            var result = _controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ValidUser_UpdatesUserAndRedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist.Add(user);
            var updatedUser = new User { Id = 1, Name = "Jane Doe", Email = "jane@example.com" };

            // Act
            var result = _controller.Edit(1, updatedUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Jane Doe", UserController.userlist[0].Name);
            Assert.Equal("jane@example.com", UserController.userlist[0].Email);
        }

        [Fact]
        public void Edit_InvalidModel_ReturnsViewWithUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist.Add(user);
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = _controller.Edit(1, user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Delete_UserExists_ReturnsViewWithUser()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = _controller.Delete(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Delete_UserDoesNotExist_ReturnsNotFound()
        {
            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteConfirmed_RemovesUserAndRedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Empty(UserController.userlist);
        }
    }
}