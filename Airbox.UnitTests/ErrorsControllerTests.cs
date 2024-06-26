﻿using Castle.Core.Logging;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Airbox.UnitTests
{
    public class ErrorsControllerTests 
    {
        private readonly Mock<IExceptionHandlerFeature> _mockFeature;
        private readonly ErrorsController _errorsController;
        private readonly Mock<ILogger<ErrorsController>> _logger;

        public ErrorsControllerTests()
        {
            _mockFeature = new Mock<IExceptionHandlerFeature>();
            _logger = new Mock<ILogger<ErrorsController>>();
            _errorsController  = new ErrorsController(_logger.Object);
            _errorsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _errorsController.ControllerContext.HttpContext.Features.Set(_mockFeature.Object);
        }

        [Fact]
        public void Index_Returns404_WhenLocationNotFoundException()
        {
            // arrange    
            _mockFeature.Setup(m => m.Error).Returns(new LocationsNotFoundException());

            // act
            var result = Assert.IsType<ObjectResult>(_errorsController.Index());
            var problemDetails = Assert.IsType<ProblemDetails>(result.Value);

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Contains("No locations found for the user.", problemDetails.Title);
        }

        [Fact]
        public void Index_Returns400_WhenValidationException()
        {
            // arrange
            _mockFeature.Setup(m => m.Error).Returns(new ValidationException("Errors"));

            // act
            var result = Assert.IsType<ObjectResult>(_errorsController.Index());
            var problemDetails = Assert.IsType<ProblemDetails>(result.Value);

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Contains("Errors", problemDetails.Title);
        }
    }
}
