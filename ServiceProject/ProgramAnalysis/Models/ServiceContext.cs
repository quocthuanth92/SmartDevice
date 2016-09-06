using LocalAccountsApp.Models;
using ProgramAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramAnalysis.Models
{
    public sealed class Global
    {
        public static ContextDataContext Context
        {
            get
            {
                string ocKey = "key_" + HttpContext.Current.GetHashCode().ToString("x");
                if (!HttpContext.Current.Items.Contains(ocKey))
                {
                    var a = new ContextDataContext();
                    a.CommandTimeout = Utility.StoreTimeOut;
                    HttpContext.Current.Items.Add(ocKey, a);
                }
                return HttpContext.Current.Items[ocKey] as ContextDataContext;
            }
        }
    }
}