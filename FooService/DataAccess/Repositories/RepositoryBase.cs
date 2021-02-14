using System;
using System.Collections.Generic;
using FooService.DataAccess.Entities;

namespace FooService.DataAccess.Repositories
{
   public class RepositoryBase<T> : IRepository<T> where T : EntityBase
   {
      private ISessionAccessor SessionAccessor { get; }

      public RepositoryBase(ISessionAccessor sessionAccessor)
      {
         SessionAccessor = sessionAccessor;
      }

      public Guid Save(T domainObject)
      {
         return SessionAccessor.Save(domainObject);
      }

      public T GetById(Guid id)
      {
         return SessionAccessor.Get<T>(id);
      }

      public IEnumerable<T> GetAll()
      {
          var ts = SessionAccessor.Session.Query<T>();
          return new List<T>(ts);
      }
   }
}
