using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _491Project.Models.DB2
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefautConnection")
        {

        }
    }
}