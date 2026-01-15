using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace TickingAppModel.Repository
{
    public class BaseClass
    {
        public BaseClass()
        {

        }
        public virtual Database GetDatabase()
        {
            Database db;
            db = DatabaseFactory.CreateDatabase();
            return db;
        }
    }
}
