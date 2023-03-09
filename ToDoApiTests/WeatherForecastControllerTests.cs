using Microsoft.Extensions.Logging;
using Moq;
using ToDoApi.Controllers;

namespace ToDoApiTests
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
        private readonly WeatherForecastController _weatherForecastController;
        public WeatherForecastControllerTests()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _weatherForecastController = new WeatherForecastController(_loggerMock.Object);

        }

        [Fact]
        public void Get_Should_LogSuccessfulVisit_AsInformation()
        {
            _weatherForecastController.Get(5);
            _loggerMock.Verify(f => f.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Visit logged"),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Fact]
        public void Get_When_UserRequests10DaysWeather_Should_LogWarning_WithMessageOf_PotentialLogRunningRequest()
        {
            _weatherForecastController.Get(12);
            _loggerMock.Verify(f => f.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Potential long running request"),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Fact]
        public void Get_When_UserRequestsMinus1DaysWeather_Should_LogError_WithMessageOf_NumberOfDaysIsNotValid()
        {
            _weatherForecastController.Get(-1);
            _loggerMock.Verify(f => f.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Number of days is not valid"),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }
    }
}