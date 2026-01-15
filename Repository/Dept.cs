using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;
namespace TickingAppModel.Repository
{
    public class Dept:BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public List<DeptModel> GetDeptList()
        {
            List<DeptModel> objList = new List<DeptModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DEPT");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.READ);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new DeptModel
                          {
                              DeptId = row.Field<int>(0),
                              DeptName = row.Field<string>(1),
                              Status = row.Field<string>(2)
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dept.cs", "GetDeptList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
        public DeptModel GetDeptByID(int ID)
        {
            DeptModel deptModel = new DeptModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DEPT");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.GET);
                db.AddInParameter(dbCommand, "@IP_intDeptId", DbType.String, ID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    deptModel.DeptId = ID;
                    deptModel.DeptName = ds.Tables[0].Rows[0][1].ToString();
                    deptModel.Status = ds.Tables[0].Rows[0][3].ToString();
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dept.cs", "GetDeptList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return deptModel;
        }
        public void AddDept(DeptModel deptModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DEPT");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.CREATE);
                db.AddInParameter(dbCommand, "@IP_strDeptName", DbType.String, deptModel.DeptName);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, deptModel.Status);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand,"@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dept.cs", "AddDept", Constant.APP_USER_ID,Constant.MACHINEIP);
            }
        }
        public void UpdateDept(DeptModel deptModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DEPT");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.UPDATE);
                db.AddInParameter(dbCommand, "@IP_intDeptId", DbType.String, deptModel.DeptId);
                db.AddInParameter(dbCommand, "@IP_strDeptName", DbType.String, deptModel.DeptName);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, deptModel.Status);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dept.cs", "UpdateDept", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void DeleteDept(int Id, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DEPT");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.DELETE);
                db.AddInParameter(dbCommand, "@IP_intDeptId", DbType.String, Id);
                db.AddInParameter(dbCommand, "@IP_IsDeleted", DbType.String, 1);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dept.cs", "AddDept", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
    }
}
