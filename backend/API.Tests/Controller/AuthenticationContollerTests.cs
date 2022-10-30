using API.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Controller
{
    public class AuthenticationContollerTests
    {
        private readonly IServiceManager _service;
        public AuthenticationContollerTests()
        {
            _service = A.Fake<IServiceManager>();
        }

        [Fact]
        public async Task AuthenticationController_LoginUser_ReturnsOk()
        {
            // Arrange
            var userForAuth = new UserForAuthenticationDto();
            var token = "sometoken";
            A.CallTo(() => _service.AuthencationService.CreateTokenAsync(userForAuth)).Returns(Task.FromResult(token));
            var controller = new AuthenticationController(_service);
            // Act
            var result = await controller.LoginUser(userForAuth);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task AuthenticationController_RegisterUser_ReturnsOk()
        {
            // Arrange 
            var userForRegistrationDto = new UserForRegistrationDto();
            A.CallTo(() => _service.AuthencationService.RegisterUserAsync(userForRegistrationDto))
                .Returns(Task.CompletedTask);
            var controller = new AuthenticationController(_service);

            // Act
            var result = await controller.RegisterUser(userForRegistrationDto);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
