using System;
using NHibernate;

namespace FooService.DataAccess
{
   public class TransactionAspect : Castle.DynamicProxy.IInterceptor
   {
      public TransactionAspect(ISessionFactory sessionFactory)
      {
         SessionFactory = sessionFactory;
      }

      public void Intercept(Castle.DynamicProxy.IInvocation invocation)
      {
         // Exception handling including retry logic omitted for brevity.
         // Note that for certain exception types and error conditions, it makes
         // sense to automatically retry the transaction. For other cases it is
         // useless.

         using var session = SessionFactory.OpenSession();
         using (var txn = session.BeginTransaction())
         {
            try
            {
               AsyncLocalSession.Value = session;
               try
               {
                  invocation.Proceed();
                  txn.Commit();
               }
               finally
               {
                  AsyncLocalSession.Value = null;
               }
            }
            catch (Exception ex)
            {
               txn.Rollback();
               throw;
            }
         }
         session.Close();
      }

      internal static System.Threading.AsyncLocal<ISession> AsyncLocalSession = new();
      // Details about AsyncLocal<T> at https://docs.microsoft.com/en-us/dotnet/api/system.threading.asynclocal-1 [Manfred]

      private ISessionFactory SessionFactory { get; }
   }
}
