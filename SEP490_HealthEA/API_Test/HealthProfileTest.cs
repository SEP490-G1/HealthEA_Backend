using Moq;
using Domain.Interfaces.IServices;
using API.Controllers.Customer;
using Domain.Models.DAO;
using Domain.Models.Common;
using Domain.Resources;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
public class MedicalRecordsControllerTests
{
    private readonly Mock<IMedicalRecordsService> _mockService;
    private readonly HealthProfileController _controller;

    public MedicalRecordsControllerTests()
    {
        _mockService = new Mock<IMedicalRecordsService>();
        _controller = new HealthProfileController(_mockService.Object);
    }

    [Fact]
    public void AddNewHealthProfile_ValidProfile_ReturnsOkResult()
    {
        // Arrange
        var profile = new HealthProfileInputDAO
        {
            FullName = "John Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Gender = 1,
            Residence = "123 Main St",
            Note = "No allergies",
            SharedStatus = 0 // Assuming 0 means private
        };

        var expectedServiceResult = new ServiceResult
        {
            devMsg = DevMsg.AddSuccess,
            userMsg = UserMsg.AddSuccess,
            statusCode = HttpStatusCode.OK,
            data = null // or the created profile data if applicable
        };

        _mockService.Setup(s => s.AddNewHealthProfile(It.IsAny<ClaimsPrincipal>(), profile))
            .Returns(expectedServiceResult);

        // Act
        var result = _controller.addNewHealthProfile(profile) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(expectedServiceResult, result.Value);
    }

    [Fact]
    public void AddNewHealthProfile_InvalidProfile_ReturnsBadRequest()
    {
        // Arrange
        var profile = new HealthProfileInputDAO
        {
            // Invalid profile with missing required fields
            FullName = null, // This should ca

        };
    }
}
