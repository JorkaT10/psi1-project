using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
       public void Intercept(IInvocation invocation)
       {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                logger.Log(ex);
            }
       }
    }
}
