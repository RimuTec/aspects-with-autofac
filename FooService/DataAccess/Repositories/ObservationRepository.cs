using FooService.DataAccess.Entities;

namespace FooService.DataAccess.Repositories
{
   public class ObservationRepository : RepositoryBase<ObservationEntity>
   {
      public ObservationRepository(ISessionAccessor sessionAccessor) : base(sessionAccessor)
      {}
   }
}
