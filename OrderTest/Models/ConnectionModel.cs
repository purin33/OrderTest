using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OrderTest.Models
{
    public class ConnectionModel
    {
        public static string ConnStr()
        {
            string Conn = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            return Conn;
        }
    }
}