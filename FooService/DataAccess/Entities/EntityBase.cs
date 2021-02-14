using System;

namespace FooService.DataAccess.Entities
{
   public class EntityBase : IHaveAuditInformation
   {
      public virtual Guid Oid { get; private set; }
      // Note that NHibernate can set property 'Id' despite the setting being private [Manfred]
      public virtual DateTime CreatedOn { get; set; }
      public virtual DateTime ModifiedOn { get; set; }
   }
}
