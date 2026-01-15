using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickingAppModel.Models;

namespace TickingAppModel.Repository
{
    class LogError : BaseClass
    {
        Database db = null;
        public override Database GetDatabase()
        {
            return base.GetDatabase();
        }
        public void ApplicationLogErrorInDB(string Source,string Msg,string PageName,string StrackTrace,int UserId,string MachineIP)
        {
            db = GetDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_LOGERRORINDB");
            db.AddInParameter(dbCommand, "i_ERROR_SOURCE", DbType.String, Source);
            db.AddInParameter(dbCommand, "i_MESSAGE", DbType.String, Msg);
            db.AddInParameter(dbCommand, "i_PAGE_NAME", DbType.String, PageName);
            db.AddInParameter(dbCommand, "i_STACK_TRACE", DbType.String, StrackTrace);
            db.AddInParameter(dbCommand, "i_CREATED_BY", DbType.Int32, UserId);
            db.AddInParameter(dbCommand, "i_MACHINEIP", DbType.String, MachineIP);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}
