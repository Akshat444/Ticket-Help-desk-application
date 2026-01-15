using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;

namespace TickingAppModel.Repository
{
    public class Login: BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public LoginModel ValidateUser(LoginModel loginModel, out string strException)
        {
            
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USERLOGIN");
                db.AddInParameter(dbCommand, "IP_strUserName", DbType.String, loginModel.strUserName.ToString());
                db.AddInParameter(dbCommand, "IP_strPassword", DbType.String, loginModel.strPassword.ToString());
                db.AddInParameter(dbCommand, "IP_strMachineIP", DbType.String, "");
                db.AddOutParameter(dbCommand, "OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    loginModel.UserId =Convert.ToInt32( ds.Tables[0].Rows[0][0].ToString());
                    loginModel.strUserName = ds.Tables[0].Rows[0][1].ToString();
                    loginModel.Role= ds.Tables[0].Rows[0][2].ToString();
                }
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "GetcategoryList", 0, "");
            }
            return loginModel;
        }
    }
}
