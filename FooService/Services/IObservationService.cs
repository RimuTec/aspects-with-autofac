using System;

namespace FooService.Services
{
   public interface IObservationService
   {
      double GetAverageTemperature(DateTime dateTime);
   }
}
