//using Domain.Interfaces.IRepositories;
//using Domain.Models.DAO;
//using Domain.Models.Entities;
//using Domain.Resources;
//using Domain.Services;
//using Moq;
//using System.Net;
//using System.Security.Claims;

//namespace Infrastructure_Test
//{
//	public class HealthProfileServiceTests
//	{
//		private readonly Mock<IMedicalRecordRepository> medicalRecordRepository;
//		private readonly Mock<IUserRepository> userRepository;
//		private readonly MedicalRecordsService service;

//		public HealthProfileServiceTests()
//		{
//			medicalRecordRepository = new Mock<IMedicalRecordRepository>();
//			userRepository = new Mock<IUserRepository>();
//			service = new MedicalRecordsService(medicalRecordRepository.Object, userRepository.Object);
//		}

//		[Fact]
//		public void AddNewHealthProfile_ShouldReturnBadRequest_WhenValidationFails()
//		{
//			var claims = new ClaimsPrincipal();
//			var profile = new HealthProfileInputDAO();
//			var expectedDevMsg = DevMsg.AddErr;
//			var expectedUserMsg = UserMsg.AddErr;

//			var result = service.AddNewHealthProfile(claims, profile);

//			Assert.Equal(HttpStatusCode.BadRequest, result.statusCode);
//			Assert.Equal(expectedDevMsg, result.devMsg);
//			Assert.Equal(expectedUserMsg, result.userMsg);
//		}

//		[Fact]
//		public void AddNewHealthProfile_ShouldReturnSuccess_WhenProfileIsValid()
//		{
//			// Arrange
//			var claims = new ClaimsPrincipal(new ClaimsIdentity(new[] {
//				new Claim(ClaimTypes.NameIdentifier, "user-id-123")
//			}));
//			var profile = new HealthProfileInputDAO
//			{
//				FullName = "John Doe",
//				DateOfBirth = new DateOnly(1990, 1, 1),
//				Gender = 1,
//				Residence = "Some City",
//				Note = "Healthy",
//			};

//			// Mock repository to return a positive result
//			medicalRecordRepository.Setup(repo => repo.AddNewHealthProfile(It.IsAny<HealthProfile>()))
//				.Returns(1);

//			// Act
//			var result = service.AddNewHealthProfile(claims, profile);

//			// Assert
//			Assert.Equal(HttpStatusCode.OK, result.statusCode);
//			Assert.Equal(DevMsg.AddSuccess, result.devMsg);
//			Assert.Equal(UserMsg.AddSuccess, result.userMsg);
//			Assert.Equal(1, result.data);
//		}

//		[Fact]
//		public void AddNewHealthProfile_ShouldReturnNotImplemented_WhenRepositoryFails()
//		{
//			var claims = new ClaimsPrincipal(new ClaimsIdentity(new[] {
//				new Claim(ClaimTypes.NameIdentifier, "user-id-123")
//			}));
//			var profile = new HealthProfileInputDAO
//			{
//				FullName = "John Doe",
//				DateOfBirth = new DateOnly(1990, 1, 1),
//				Gender = 1,
//				Residence = "Some City",
//				Note = "Healthy",
//			};

//			medicalRecordRepository.Setup(repo => repo.AddNewHealthProfile(It.IsAny<HealthProfile>()))
//				.Returns(0);

//			var result = service.AddNewHealthProfile(claims, profile);

//			Assert.Equal(HttpStatusCode.NotImplemented, result.statusCode);
//			Assert.Equal(DevMsg.AddErr, result.devMsg);
//			Assert.Equal(UserMsg.AddErr, result.userMsg);
//			Assert.Equal(0, result.data);
//		}

//		[Fact]
//		public void AddNewHealthProfile_ShouldThrowException_WhenClaimsAreInvalid()
//		{
//			var claims = new ClaimsPrincipal(); 
//			var profile = new HealthProfileInputDAO
//			{
//				FullName = "John Doe",
//				DateOfBirth = new DateOnly(1990, 1, 1),
//				Gender = 1,
//				Residence = "Some City",
//				Note = "Healthy",
//			};

//			var exception = Assert.Throws<InvalidOperationException>(() =>
//				service.AddNewHealthProfile(claims, profile)
//			);

//			Assert.Equal("Invalid claims or user ID", exception.Message);
//		}
//	}
//}
