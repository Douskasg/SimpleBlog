using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TransactionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}