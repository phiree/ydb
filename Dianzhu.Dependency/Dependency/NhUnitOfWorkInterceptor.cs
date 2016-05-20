using System.Reflection;
using Castle.DynamicProxy;

using NHibernate;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

using System;
using Dianzhu.DAL;
using Dianzhu.IDAL;

namespace Dianzhu.DependencyInstaller
{
    /// <summary>
    /// This interceptor is used to manage transactions.
    /// </summary>
    public class NhUnitOfWorkInterceptor : IInterceptor
    {
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Creates a new NhUnitOfWorkInterceptor object.
        /// </summary>
        /// <param name="sessionFactory">Nhibernate session factory.</param>
        public NhUnitOfWorkInterceptor(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Intercepts a method.
        /// </summary>
        /// <param name="invocation">Method invocation arguments</param>
        public void Intercept(IInvocation invocation)
        {
            //If there is a running transaction, just run the method
            if (NHUnitOfWork.Current != null || !RequiresDbConnection(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                NHUnitOfWork.Current = new NHUnitOfWork(_sessionFactory);
                NHUnitOfWork.Current.BeginTransaction();

                try
                {
                    invocation.Proceed();
                    NHUnitOfWork.Current.Commit();
                }
                catch
                {
                    try
                    {
                        NHUnitOfWork.Current.Rollback();
                    }
                    catch
                    {

                    }

                    throw;
                }
            }
            finally
            {
                NHUnitOfWork.Current = null;
            }
        }

        private static bool RequiresDbConnection(MethodInfo methodInfo)
        {
            if (UnitOfWorkHelper.HasUnitOfWorkAttribute(methodInfo))
            {
                return true;
            }

            if (UnitOfWorkHelper.IsRepositoryMethod(methodInfo))
            {
                return true;
            }

            return false;
        }
    }

    public static class UnitOfWorkHelper
    {
        public static bool IsRepositoryMethod(MethodInfo methodInfo)
        {
            return IsRepositoryClass(methodInfo.DeclaringType);
        }

        public static bool IsRepositoryClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type);
        }

        public static bool HasUnitOfWorkAttribute(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}