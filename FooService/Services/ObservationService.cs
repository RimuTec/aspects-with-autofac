using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FooService.DataAccess.Entities;
using FooService.DataAccess.Repositories;

namespace FooService.Services
{
   public class ObservationService : IObservationService
   {
      public ObservationService(IRepository<ObservationEntity> observationRepository)
      {
         _observationRepository = observationRepository;
      }

      public double GetAverageTemperature(DateTime dateTime)
      {
         var comparer = new DateComparer();
         var observations = _observationRepository.GetAll().Where(o => comparer.Equals(dateTime, o.ObservedOn));
         var average = observations.Aggregate(0.0d, (sum, next) => sum + next.Temperature) / observations.Count();
         return average;
      }

      private class DateComparer : IEqualityComparer<DateTime>
      {
         public bool Equals(DateTime x, DateTime y)
         {
            return x.Year == y.Year
            && x.Month == y.Month
            && x.Day == y.Day;
         }

         public int GetHashCode([DisallowNull] DateTime obj)
         {
            return new DateTime(obj.Year, obj.Month, obj.Day).GetHashCode();
         }
      }
      private readonly IRepository<ObservationEntity> _observationRepository;
   }
}
