using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;

namespace TickingAppModel.Repository
{
    public class Category:BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public List<CategoryModel> GetcategoryList()
        {
            List<CategoryModel> objList = new List<CategoryModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.READ);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new CategoryModel
                          {
                              CategoryId = row.Field<int>(0),
                              CategoryName = row.Field<string>(1),
                              Status = row.Field<string>(2),
                              DeptName= row.Field<string>(3)
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "GetcategoryList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
        public CategoryModel GetCategoryByID(int ID)
        {
            CategoryModel categoryModel = new CategoryModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.GET);
                db.AddInParameter(dbCommand, "@IP_intCategoryId", DbType.String, ID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    categoryModel.CategoryId = ID;
                    categoryModel.CategoryName = ds.Tables[0].Rows[0][1].ToString();
                    categoryModel.Status = ds.Tables[0].Rows[0][3].ToString();
                    categoryModel.DeptId =Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "GetCategoryList", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return categoryModel;
        }
        public void AddCategory(CategoryModel categoryModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.CREATE);
                db.AddInParameter(dbCommand, "@IP_strCategoryName", DbType.String, categoryModel.CategoryName);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, categoryModel.Status);
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, categoryModel.DeptId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "AddCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void UpdateCategory(CategoryModel categoryModel, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.UPDATE);
                db.AddInParameter(dbCommand, "@IP_intCategoryId", DbType.String, categoryModel.CategoryId);
                db.AddInParameter(dbCommand, "@IP_strCategoryName", DbType.String, categoryModel.CategoryName);
                db.AddInParameter(dbCommand, "@IP_IsActive", DbType.String, categoryModel.Status);
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, categoryModel.DeptId);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "UpdateCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }
        public void DeleteCategory(int Id, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.DELETE);
                db.AddInParameter(dbCommand, "@IP_intCategoryId", DbType.String, Id);
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
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "DeleteCategory", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
        }

        public List<CategoryModel> GetcategoryListByDept(int? DeptID)
        {
            List<CategoryModel> objList = new List<CategoryModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CATEGORY");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, "GETBYDEPT");
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, DeptID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new CategoryModel
                          {
                              CategoryId = row.Field<int>(0),
                              CategoryName = row.Field<string>(1),
                              Status = row.Field<string>(2),
                              DeptId = row.Field<int>(4)
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Category.cs", "GetcategoryListByDept", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
    }
}
