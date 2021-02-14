using System;
using NHibernate;
using FooService.DataAccess.Entities;
using System.Linq;

namespace FooService.DataAccess
{
   public interface ISessionAccessor
   {
      /// <summary>
      /// Strongly-typed version of NHibernate.ISession.Get(System.Type,System.Guid)
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="id"></param>
      /// <returns>A T if found, null otherwise</returns>
      T Get<T>(Guid id) where T : EntityBase;

      /// <summary>
      /// Persist the given transient instance, first assigning a generated identifier.
      /// </summary>
      /// <param name="domainObject">A transient instance of a persistent class</param>
      /// <returns>The generated identifier</returns>
      /// <remarks>Save will use the current value of the identifier property if the Assigned generator
      /// is used.</remarks>
      Guid Save(EntityBase domainObject);

      IQueryable<T> Query<T>();

      ISession Session { get; }
   }
}
