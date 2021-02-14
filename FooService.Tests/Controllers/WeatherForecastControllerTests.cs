using System.Linq;
using FooService.Controllers;
using FooService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FooService.Tests
{
   public class WeatherForecastControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get()
        {
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var mockRepository = new Mock<IObservationService>();
            var controller = new WeatherForecastController(mockRepository.Object, mockLogger.Object);
            Assert.AreEqual(5, controller.Get().Count());
        }
    }
}
