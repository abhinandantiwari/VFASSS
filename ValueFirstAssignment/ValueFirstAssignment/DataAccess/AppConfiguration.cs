using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ValueFirstAssignment.DataAccess
{
    public static class AppConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;
            }
        }
    }
}