using System;
using System.Linq;
using FooService.DataAccess.Entities;
using FooService.DataAccess.Repositories;

namespace FooService
{
   public class Seeder
   {
      public Seeder(IRepository<ObservationEntity> observationRepository)
      {
         _observationRepository = observationRepository;
      }

      public virtual void Run()
      {
         var now = DateTime.Now;
         var observations = _observationRepository.GetAll();
         if(observations.Count(o => o.ObservedOn > now) < 24)
         {
            var observationCount = 6 * 4; // enough for 6 days
            var rng = new Random();
            while(observationCount-- > 0)
            {
               var date = DateTime.Now.AddHours(observationCount * 6);
               var observation = new ObservationEntity
               {
                  ObservedOn = date,
                  Temperature = rng.Next(-20, 55)
               };
               _observationRepository.Save(observation);
            }
         }
      }

      private readonly IRepository<ObservationEntity> _observationRepository;
   }
}
