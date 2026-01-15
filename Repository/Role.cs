using System;
using System.Collections.Generic;
using System.Linq;
using TickingAppModel.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
namespace TickingAppModel.Repository
{
    public class Role : BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public List<RoleListingDT> GetRoleList()
        {
            List<RoleListingDT> objList = new List<RoleListingDT>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_FETCH_ROLEDETAIL");
                db.AddInParameter(dbCommand, "@IP_INT_UserID", DbType.Int32, "0");
                db.AddOutParameter(dbCommand, "@OP_STR_Exception", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new RoleListingDT
                          {
                            RoleID = row.Field<int?>(0).GetValueOrDefault(),
                            RoleName = row.Field<string>(1),
                            Status = row.Field<string>(2)
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Role.cs", "GetRoleList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
        public void DeleteRole(int RoleId, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_ADDEDIT_ROLE");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, "DELETE");
                db.AddInParameter(dbCommand, "@IP_intRoleID", DbType.Int32, RoleId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Role.cs", "DeleteRole", 0, "");
            }
        }
        public void CreateRole(RoleCreation role, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_ADDEDIT_ROLE");         
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.CREATE);
                db.AddInParameter(dbCommand, "@IP_strRoleName", DbType.String, role.RoleName);
                db.AddInParameter(dbCommand, "@IP_intIsActive", DbType.String, role.Status);
                db.AddInParameter(dbCommand, "@IP_strModuleID", DbType.String, role.ModuleIds);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Role.cs", "CreateRole", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void UpdateRole(RoleCreation role, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_ADDEDIT_ROLE");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.UPDATE);
                db.AddInParameter(dbCommand, "@IP_intRoleID", DbType.String, role.RoleID);
                db.AddInParameter(dbCommand, "@IP_strRoleName", DbType.String, role.RoleName);
                db.AddInParameter(dbCommand, "@IP_intIsActive", DbType.String, role.Status);
                db.AddInParameter(dbCommand, "@IP_strModuleID", DbType.String, role.ModuleIds);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Role.cs", "UpdateRole", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public RoleCreation GetRole(int Id, out string strException)
        {
            RoleCreation obj = new RoleCreation();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_ADDEDIT_ROLE");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.READ);
                db.AddInParameter(dbCommand, "@IP_intRoleID", DbType.String, Id);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.RoleID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    obj.RoleName = ds.Tables[0].Rows[0][1].ToString();
                    obj.Status = ds.Tables[0].Rows[0][2].ToString();
                }
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Role.cs", "GetRole", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return obj;
        }
        public List<NavbarModel> GetModuleByRole(int RoleId, out string strException)
        {
            List<NavbarModel> objList = new List<NavbarModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_ADDEDIT_ROLE");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, "GET_MODULE");
                db.AddInParameter(dbCommand, "@IP_intRoleID", DbType.Int32, RoleId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new NavbarModel
                          {
                              ModuleId = row.Field<int>(0),
                              ModuleName = row.Field<string>(1),
                              URL = row.Field<string>(2),
                              Iconimg = row.Field<string>(3),
                              ParentModuleId = row.Field<int>(4),
                              Status = row.Field<int>(5),
                          }).ToList();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Role.cs", "GetModuleByRole", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
    }
}
