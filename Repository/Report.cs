using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;

namespace TickingAppModel.Repository
{
    public class Report: BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();
        public List<TATReportModel> TATReport()
        {
            List<TATReportModel> objList = new List<TATReportModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_TAT_REPORT");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new TATReportModel
                          {
                              TicketId = row.Field<int>(0),
                              TicketNo = row.Field<string>(1),
                              Department = row.Field<string>(2),
                              Category = row.Field<string>(3),
                              SubCategory = row.Field<string>(4),
                              Status = row.Field<string>(5),
                              Priority = row.Field<string>(6),
                              TDate = row.Field<string>(7),
                              TicketTransfer = row.Field<string>(8),
                              ClosingDate = row.Field<string>(9),
                              TAT = row.Field<string>(10),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Report.cs", "TATReport", 0, "");
            }
            return objList;
        }
        public List<DetailReportModel> DetailReport()
        {
            List<DetailReportModel> objList = new List<DetailReportModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DETAIL_REPORT");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new DetailReportModel
                          {
                              TicketId = row.Field<int>(0),
                              TicketNo = row.Field<string>(1),
                              Department = row.Field<string>(2),
                              Category = row.Field<string>(3),
                              SubCategory = row.Field<string>(4),
                              Status = row.Field<string>(5),
                              Priority = row.Field<string>(6),
                              TDate = row.Field<string>(7),
                              Comments = row.Field<string>(8),
                              Attachment = row.Field<string>(9),
                              TicketTransfer = row.Field<string>(10),
                              Closingdate = row.Field<string>(11),
                              ClosedBy = row.Field<string>(12),
                              IssuedBy = row.Field<string>(13),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Report.cs", "DetailReport", 0, "");
            }
            return objList;
        }
    }
}
