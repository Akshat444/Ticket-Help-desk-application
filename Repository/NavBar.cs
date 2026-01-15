using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;

namespace TickingAppModel.Repository
{
    public class NavBar: BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();

        public List<NavbarModel> BindMenu()
        {
            List<NavbarModel> objList = new List<NavbarModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_MENU");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new NavbarModel
                          {
                              ModuleId = row.Field<int>(0),
                              ModuleName = row.Field<string>(1),
                              URL = row.Field<string>(2),
                              Iconimg = row.Field<string>(3),
                              ParentModuleId = row.Field<int>(4),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "NavBar.cs", "BindMenu", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
    }
}
