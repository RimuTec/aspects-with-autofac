using System;
using System.Collections.Generic;
using FooService.DataAccess.Entities;

namespace FooService.DataAccess.Repositories
{
   public interface IRepository<T> where T : EntityBase
   {
      T GetById(Guid id);
      Guid Save(T template);

      IEnumerable<T> GetAll();
   }
}
