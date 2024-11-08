using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Infrastructure.MediatR.Events.Commands.DeleteEvent;
using Infrastructure.MediatR.Events.Commands.UpdateEvent;
using Infrastructure.MediatR.Events.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using API.Controllers;
using FluentAssertions;

namespace API.Tests.Controllers
{
    public class EventsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EventsController _controller;

        public EventsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EventsController(_mediatorMock.Object);
        }

        #region GetEvents
        //[Fact]
        //public async Task GetEvents_ReturnsOk_WhenEventsExist()
        //{
        //    // Arrange
        //    var startDate = DateTime.UtcNow.AddDays(-1);
        //    var endDate = DateTime.UtcNow.AddDays(1);
        //    var eventsList = new List<EventDto>
        //    {
        //        new EventDto { EventId = Guid.NewGuid(), Title = "Test Event" }
        //    };

        //    _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventsQuery>(), It.IsAny<CancellationToken>()))
        //                 .ReturnsAsync(eventsList);

        //    // Act
        //    var result = await _controller.GetEvents(startDate, endDate) as OkObjectResult;

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.StatusCode.Should().Be(200);

        //    var response = result.Value as Dictionary<string, object>;
        //    response.Should().NotBeNull();
        //    response!.ContainsKey("Events").Should().BeTrue();

        //    var events = response["Events"] as List<EventDto>;
        //    events.Should().NotBeNull().And.HaveCount(1);
        //}

        [Fact]
        public async Task GetEvents_ReturnsNotFound_WhenNoEventsExist()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddDays(1);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new List<EventDto>());

            // Act
            var result = await _controller.GetEvents(startDate, endDate) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
        #endregion

        #region CreateEvent
        //[Fact]
        //public async Task CreateEvent_ReturnsOk_WhenEventCreated()
        //{
        //    var command = new CreateEventCommand { Title = "New Event" };
        //    var newEventId = Guid.NewGuid();

        //    _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()))
        //                 .ReturnsAsync(newEventId);

        //    var result = await _controller.CreateEvent(command, CancellationToken.None) as OkObjectResult;

        //    result.Should().NotBeNull("Expected OkObjectResult but got null");

        //    result?.StatusCode.Should().Be(200);

        //    var response = result?.Value as Dictionary<string, object>;
        //    response.Should().NotBeNull("Expected response dictionary but got null");
        //    response!.ContainsKey("OriginEventId").Should().BeTrue();

        //    var originEventId = response["OriginEventId"];
        //    originEventId.Should().Be(newEventId);
        //}


        #endregion

        #region UpdateEvent
        //[Fact]
        //public async Task UpdateEvent_ReturnsOk_WhenEventUpdated()
        //{
        //    // Arrange
        //    var eventId = Guid.NewGuid();
        //    var command = new UpdateEventCommand { EventId = eventId, Title = "Updated Event" };
        //    _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>()))
        //                 .ReturnsAsync(eventId);

        //    // Act
        //    var result = await _controller.UpdateEvent(eventId, command, CancellationToken.None) as OkObjectResult;

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.StatusCode.Should().Be(200);

        //    var response = result.Value as Dictionary<string, object>;
        //    response.Should().NotBeNull();
        //    response["EventId"].Should().Be(eventId);
        //}

        [Fact]
        public async Task UpdateEvent_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var command = new UpdateEventCommand { EventId = Guid.NewGuid(), Title = "Updated Event" };

            // Act
            var result = await _controller.UpdateEvent(eventId, command, CancellationToken.None) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }
        #endregion

        #region DeleteEvent
        [Fact]
        public async Task DeleteEvent_ReturnsOk_WhenEventDeleted()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteEvent(eventId, CancellationToken.None) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteEvent_ReturnsNotFound_WhenEventNotFound()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteEvent(eventId, CancellationToken.None) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
        #endregion
    }
}
