using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Infrastructure
{
    public class TransactionFilter : IActionFilter
    {
         void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Database.Session.BeginTransaction();
        }

   
        [Obsolete]
         public void OnActionExecuted(ActionExecutedContext filterContext)
         {
             if (filterContext.Exception == null)
                 Database.Session.Transaction.Commit();
             else
                 Database.Session.Transaction.Rollback();
           }


    }
}