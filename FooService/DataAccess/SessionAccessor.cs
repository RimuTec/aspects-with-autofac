using NHibernate;
using System;
using FooService.DataAccess.Entities;
using System.Linq;

namespace FooService.DataAccess
{
   public class SessionAccessor : ISessionAccessor
   {
      public T Get<T>(Guid id) where T : EntityBase
      {
         return Session.Get<T>(id);
      }

      public Guid Save(EntityBase domainObject)
      {
         return (Guid) Session.Save(domainObject);
      }

      public IQueryable<T> Query<T>()
      {
         return Session.Query<T>();
      }

      public ISession Session
      {
         get
         {
            return TransactionAspect.AsyncLocalSession.Value;
         }
      }
   }
}
