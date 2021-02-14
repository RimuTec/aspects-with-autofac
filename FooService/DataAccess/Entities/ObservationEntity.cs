using System;

namespace FooService.DataAccess.Entities
{
   public class ObservationEntity : EntityBase
   {
      public virtual DateTime ObservedOn { get; set; }
      public virtual double Temperature { get; set; }
   }
}
