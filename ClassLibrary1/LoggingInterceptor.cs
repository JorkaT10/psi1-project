using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using PSI_MobileApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IInterceptor = Castle.DynamicProxy.IInterceptor;
namespace ClassLibrary
{
    public class LoggingInterceptor : Attribute, IInterceptor
    {
       ExceptionLogger logger = new();
       private ProjectDatabaseContext _context;
       public void Intercept(IInvocation invocation)
       {
            try
            {
                _context.TestConnection();
                invocation.Proceed();
            }
            catch(Exception ex)
            {
                invocation.ReturnValue = null;
                logger.Log(ex);
            }
       }
        public LoggingInterceptor(ProjectDatabaseContext context)
        {
            _context = context;
        }
    }
}
