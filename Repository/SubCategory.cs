using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;

namespace TickingAppModel.Repository
{
    public class SubCategory : BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public List<SubCategoryModel> GetSubCategoryList()
        {
            List<SubCategoryModel> objList = new List<SubCategoryModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_SUBCATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String,Constant.READ);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new SubCategoryModel
                          {
                              SubCategoryId = row.Field<int>(0),
                              SubCategoryName = row.Field<string>(1),
                              Status = row.Field<string>(2),
                              CategoryName = row.Field<string>(3),
                              DeptName= row.Field<string>(4),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "SubCategory.cs", "GetSubCategoryList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
        public SubCategoryModel GetSubCategoryByID(int ID)
        {
            SubCategoryModel subCategoryModel = new SubCategoryModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_SUBCATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.GET);
                db.AddInParameter(dbCommand, "@IP_intSubCategoryId", DbType.String, ID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    subCategoryModel.SubCategoryId = ID;
                    subCategoryModel.SubCategoryName = ds.Tables[0].Rows[0][1].ToString();
                    subCategoryModel.Status = ds.Tables[0].Rows[0][3].ToString();
                    subCategoryModel.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "SubCategory.cs", "GetSubCategoryByID", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return subCategoryModel;
        }
        public void AddSubCategory(SubCategoryModel subCategoryModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_SUBCATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.CREATE);
                db.AddInParameter(dbCommand, "@IP_strSubCategoryName", DbType.String, subCategoryModel.SubCategoryName);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, subCategoryModel.Status);
                db.AddInParameter(dbCommand, "@IP_intCategoryID", DbType.String, subCategoryModel.CategoryId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "SubCategory.cs", "AddSubCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void UpdateSubCategory(SubCategoryModel subCategoryModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_SUBCATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.UPDATE);
                db.AddInParameter(dbCommand, "@IP_intSubCategoryId", DbType.String, subCategoryModel.SubCategoryId);
                db.AddInParameter(dbCommand, "@IP_strSubCategoryName", DbType.String, subCategoryModel.SubCategoryName);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, subCategoryModel.Status);
                db.AddInParameter(dbCommand, "@IP_intCategoryID", DbType.String, subCategoryModel.CategoryId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "SubCategory.cs", "UpdateSubCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void DeleteSubCategory(int Id, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_SUBCATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.DELETE);
                db.AddInParameter(dbCommand, "@IP_intSubCategoryId", DbType.String, Id);
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
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "SubCategory.cs", "DeleteSubCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public List<SubCategoryModel> GetSubCategoryListByCategory(int? CatgeoryId)
        {
            List<SubCategoryModel> objList = new List<SubCategoryModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_SUBCATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, "GETBYCATEGORY");
                db.AddInParameter(dbCommand, "@IP_intCategoryID", DbType.Int32, CatgeoryId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new SubCategoryModel
                          {
                              SubCategoryId = row.Field<int>(0),
                              SubCategoryName = row.Field<string>(1),
                              Status = row.Field<string>(2)
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "SubCategory.cs", "GetSubCategoryListByCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
    }
}
