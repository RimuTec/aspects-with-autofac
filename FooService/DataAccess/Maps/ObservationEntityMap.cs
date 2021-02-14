using FluentNHibernate.Mapping;
using FooService.DataAccess.Entities;

namespace FooService.DataAccess.Maps
{
   internal class ObservationEntityMap : ClassMap<ObservationEntity>
   {
      public ObservationEntityMap()
      {
         Table("Observation");

         Id(o => o.Oid);
         Map(o => o.CreatedOn );
         Map(o => o.ModifiedOn);

         Map(o => o.ObservedOn);
         Map(o => o.Temperature);
      }
   }
}
