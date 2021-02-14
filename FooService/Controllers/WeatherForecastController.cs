using System;
using System.Collections.Generic;
using System.Linq;
using FooService.Models;
using FooService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FooService.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class WeatherForecastController : ControllerBase
   {
      private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      private readonly ILogger<WeatherForecastController> _logger;

      public WeatherForecastController(IObservationService observationService, ILogger<WeatherForecastController> logger)
      {
         _observationService = observationService;
         _logger = logger;
      }

      [HttpGet]
      public virtual IEnumerable<WeatherForecastModel> Get()
      {
         var rng = new Random();
         return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
         {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)],
            AverageTemperature = _observationService.GetAverageTemperature(DateTime.Now.AddDays(index))
         })
         .ToArray();
      }

      private readonly IObservationService _observationService;
   }
}
