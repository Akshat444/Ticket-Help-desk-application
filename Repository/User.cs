using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;
namespace TickingAppModel.Repository
{
    public class User:BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public List<UserModelList> GetUserList()
        {
            List<UserModelList> objList = new List<UserModelList>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USER");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.READ);
                db.AddInParameter(dbCommand, "@IP_intLoginUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new UserModelList
                          {
                              UserId = row.Field<int>(0),
                              UserName = row.Field<string>(1),
                              Name = row.Field<string>(2),
                              DeptName = row.Field<string>(3),
                              MobileNo = row.Field<string>(4),
                              Email = row.Field<string>(5),
                              Status = row.Field<string>(6),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "GetUserList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
        public UserModel GetUserDetail(int ID)
        {
            UserModel userModel = new UserModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USER");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.GET);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.String, ID);
                db.AddInParameter(dbCommand, "@IP_intLoginUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand,"@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    userModel.UserId = ID;
                    userModel.UserName = ds.Tables[0].Rows[0][1].ToString();
                    userModel.FirstName = ds.Tables[0].Rows[0][2].ToString();
                    userModel.MiddleName = ds.Tables[0].Rows[0][3].ToString();
                    userModel.LastName = ds.Tables[0].Rows[0][4].ToString();
                    userModel.DeptId =Convert.ToInt32( ds.Tables[0].Rows[0][5].ToString());
                    userModel.RoleId = Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString());
                    userModel.Designation = ds.Tables[0].Rows[0][7].ToString();
                    userModel.MobileNo = ds.Tables[0].Rows[0][8].ToString();
                    userModel.Email = ds.Tables[0].Rows[0][9].ToString();
                    userModel.Status= ds.Tables[0].Rows[0][10].ToString();
                    userModel.Password = ds.Tables[0].Rows[0][11].ToString();
                    userModel.ConfirmPassword = userModel.Password;
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "GetUserDetail", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return userModel;
        }
        public void CreateUser(UserModel userModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USER");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.CREATE);
                db.AddInParameter(dbCommand, "@IP_strFirstName", DbType.String, userModel.FirstName);
                db.AddInParameter(dbCommand, "@IP_strMiddleName", DbType.String, userModel.MiddleName);
                db.AddInParameter(dbCommand, "@IP_strLastName", DbType.String, userModel.LastName);
                db.AddInParameter(dbCommand, "@IP_strUserName", DbType.String, userModel.UserName);
                db.AddInParameter(dbCommand, "@IP_strPassword", DbType.String, userModel.Password);
                db.AddInParameter(dbCommand, "@IP_strMobile", DbType.String, userModel.MobileNo);
                db.AddInParameter(dbCommand, "@IP_strEmail", DbType.String, userModel.Email);
                db.AddInParameter(dbCommand, "@IP_strDesignation", DbType.String, userModel.Designation);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, userModel.Status);
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, userModel.DeptId);
                db.AddInParameter(dbCommand, "@IP_intRoleID", DbType.Int32, userModel.RoleId);
                db.AddInParameter(dbCommand, "@IP_intLoginUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand,"@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "CreateUser", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void UpdateUser(UserModel userModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USER");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.UPDATE);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.String, userModel.UserId);
                db.AddInParameter(dbCommand, "@IP_strFirstName", DbType.String, userModel.FirstName);
                db.AddInParameter(dbCommand, "@IP_strMiddleName", DbType.String, userModel.MiddleName);
                db.AddInParameter(dbCommand, "@IP_strLastName", DbType.String, userModel.LastName);
                db.AddInParameter(dbCommand, "@IP_strMobile", DbType.String, userModel.MobileNo);
                db.AddInParameter(dbCommand, "@IP_strEmail", DbType.String, userModel.Email);
                db.AddInParameter(dbCommand, "@IP_strDesignation", DbType.String, userModel.Designation);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, userModel.Status);
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, userModel.DeptId);
                db.AddInParameter(dbCommand, "@IP_intRoleID", DbType.Int32, userModel.RoleId);
                db.AddInParameter(dbCommand, "@IP_intLoginUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "CreateUser", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void DeleteUser(int ID, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USER");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.DELETE);        
                db.AddInParameter(dbCommand, "@IP_IsDeleted", DbType.String, 1);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.String, ID);
                db.AddInParameter(dbCommand, "@IP_intLoginUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "DeleteUser", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void ChnagePassword(UserChangePassword ucp, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CHANGE_PWD");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.String, ucp.intLoginUserID);
                db.AddInParameter(dbCommand, "@IP_strCurrentPWD", DbType.String, ucp.CurrentPassword);
                db.AddInParameter(dbCommand, "@IP_strNewPWD", DbType.String, ucp.NewPassword);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, ucp.strMachineIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "ChnagePassword", ucp.intLoginUserID, ucp.strMachineIP);
            }
        }
        public List<DDLUserModel> GetUserListByDept(int? DeptID)
        {
            List<DDLUserModel> objList = new List<DDLUserModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_USER");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, "USER_DEPT");
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, DeptID);
                db.AddInParameter(dbCommand, "@IP_intLoginUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new DDLUserModel
                          {
                              UserId = row.Field<int>(0),
                              UserName = row.Field<string>(1)
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "GetUserListByDept", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }

        public void checkUserModule(string strPageURL, out int strException) 
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CHECK_USER_MODULE");
                db.AddInParameter(dbCommand, "@IP_strPageURL", DbType.String, strPageURL);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.String, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand,"@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException =Convert.ToInt32( db.GetParameterValue(dbCommand, "@OP_strException"));
            }
            catch (Exception ex)
            {
                strException = 0;
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "User.cs", "checkUserModule", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
    }
}
