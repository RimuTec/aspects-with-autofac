using System;

namespace FooService.DataAccess.Entities
{
   public interface IHaveAuditInformation
   {
      DateTime CreatedOn { get; set; }
      DateTime ModifiedOn { get; set; }
   }
}
