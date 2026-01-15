using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;
namespace TickingAppModel.Repository
{
    public class Ticket:BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();

        public List<TicketModelList> GetTicketList()
        {
            List<TicketModelList> objList = new List<TicketModelList>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_TICKET");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.READ);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new TicketModelList
                          {
                              TicketId = row.Field<int>(0),
                              TicketNo = row.Field<string>(1),
                              Department = row.Field<string>(2),
                              Category = row.Field<string>(3),
                              SubCategory = row.Field<string>(4),
                              Status = row.Field<string>(5),
                              Priority = row.Field<string>(6),
                              TDate = row.Field<string>(7),
                              Waiting= row.Field<string>(8),
                              Attachment = row.Field<int>(9),
                              TicketTransfer= row.Field<string>(10),
                              Feedback = row.Field<string>(11),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "GetTicketList", 0, "");
            }
            return objList;
        }
        public void CreateTicket(TicketModel ticketModel,out string strTicketNo, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_CREATE_TICKET");
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.Int32, ticketModel.DeptId);
                db.AddInParameter(dbCommand, "@IP_intCategoryID", DbType.Int32, ticketModel.CategoryId);
                db.AddInParameter(dbCommand, "@IP_intSubCategoryID", DbType.Int32, ticketModel.SubCategoryId);
                db.AddInParameter(dbCommand, "@IP_strPriority", DbType.String, ticketModel.Priority);
                
                db.AddInParameter(dbCommand, "@IP_strComments", DbType.String, ticketModel.Comments);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddInParameter(dbCommand, "@IP_strFileName", DbType.String, ticketModel.FileName);

                db.AddOutParameter(dbCommand, "@OP_strTicketNo", DbType.String, 500);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strTicketNo = db.GetParameterValue(dbCommand, "@OP_strTicketNo").ToString();
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                strTicketNo = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "CreateTicket", 0, "");
            }
        }
        public ViewTicketModel ViewTicket(int intTicketID)
        {
            ViewTicketModel obj = new ViewTicketModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_TICKET");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.GET);
                db.AddInParameter(dbCommand, "@IP_intTicketID", DbType.Int32, intTicketID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.TicketId =intTicketID;
                    obj.TicketNo = ds.Tables[0].Rows[0][1].ToString();
                    obj.TicketDate = ds.Tables[0].Rows[0][2].ToString();
                    obj.IssueBy = ds.Tables[0].Rows[0][3].ToString();
                    obj.Department = ds.Tables[0].Rows[0][4].ToString();
                    obj.Category = ds.Tables[0].Rows[0][5].ToString();
                    obj.SubCategory = ds.Tables[0].Rows[0][6].ToString();
                    obj.Feedback = ds.Tables[0].Rows[0][7].ToString();
                    obj.Status = ds.Tables[0].Rows[0][8].ToString();
                    obj.Action = ds.Tables[0].Rows[0][9].ToString();
                    obj.Remark = ds.Tables[0].Rows[0][10].ToString();
                    obj.Attachment = ds.Tables[0].Rows[0][11].ToString();
                    obj.TicketTransfer = ds.Tables[0].Rows[0][12].ToString();
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "GetTicketList", 0, "");
            }
            return obj;
        }
        public TransferTicketModel TransferTicket(int intTicketID)
        {
            TransferTicketModel obj = new TransferTicketModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_TICKET");
                db.AddInParameter(dbCommand, "@IP_strAction", DbType.String, Constant.GET);
                db.AddInParameter(dbCommand, "@IP_intTicketID", DbType.Int32, intTicketID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.TicketId = intTicketID;
                    obj.TicketNo = ds.Tables[0].Rows[0][1].ToString();
                    obj.TicketDate = ds.Tables[0].Rows[0][2].ToString();
                    obj.IssueBy = ds.Tables[0].Rows[0][3].ToString();
                    obj.Department = ds.Tables[0].Rows[0][4].ToString();
                    obj.Category = ds.Tables[0].Rows[0][5].ToString();
                    obj.SubCategory = ds.Tables[0].Rows[0][6].ToString();
                    obj.Feedback = ds.Tables[0].Rows[0][7].ToString();
                    //obj.Status = ds.Tables[0].Rows[0][8].ToString();
                    //obj.Action = ds.Tables[0].Rows[0][9].ToString();
                    //obj.Remark = ds.Tables[0].Rows[0][10].ToString();
                    obj.Attachment = ds.Tables[0].Rows[0][11].ToString();
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "GetTicketList", 0, "");
            }
            return obj;
        }
        public void UpdateTicketStatus(ViewTicketModel viewTicket, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_UPDATE_TICKET");
                db.AddInParameter(dbCommand, "@IP_intTicketID", DbType.Int32, viewTicket.TicketId);
                db.AddInParameter(dbCommand, "@IP_Status", DbType.Int32, viewTicket.Action);
                db.AddInParameter(dbCommand, "@IP_strRemark", DbType.String, viewTicket.Remark);               
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "UpdateTicketStatus", 0, "");
            }
        }
        public void TransferTicketStatus(TransferTicketModel ticket, out string strException)
        {
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_TRANSFER_TICKET");
                db.AddInParameter(dbCommand, "@IP_intTicketID", DbType.Int32, ticket.TicketId);
                db.AddInParameter(dbCommand, "@IP_intTransferUserID", DbType.Int32, ticket.UserId);
                db.AddInParameter(dbCommand, "@IP_intDeptID", DbType.String, ticket.DeptId);
                db.AddInParameter(dbCommand, "@IP_strRemark", DbType.String, ticket.Remark);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                db.ExecuteNonQuery(dbCommand);
                strException = db.GetParameterValue(dbCommand, "@OP_strException").ToString();
            }
            catch (Exception ex)
            {
                strException = "";
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "TransferTicketStatus", 0, "");
            }
        }

        public List<TrackTicketModel> TrackTicket()
        {
            List<TrackTicketModel> objList = new List<TrackTicketModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_TRACK_TICKET");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                objList = ds.Tables[0].AsEnumerable()
                          .Select(row => new TrackTicketModel
                          {
                              TicketId = row.Field<int>(0),
                              TicketNo = row.Field<string>(1),
                              Department = row.Field<string>(2),
                              Category = row.Field<string>(3),
                              SubCategory = row.Field<string>(4),
                              Status = row.Field<string>(5),
                              Priority = row.Field<string>(6),
                              TDate = row.Field<string>(7),
                              Waiting = row.Field<string>(8),
                              Attachment = row.Field<int>(9),
                              TicketTransfer = row.Field<string>(10),
                              Comments = row.Field<string>(11),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "TrackTicket", 0, "");
            }
            return objList;
        }
        public ViewTicketModel TrackTicketDetail(int intTicketID)
        {
            ViewTicketModel obj = new ViewTicketModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_TRACK_TICKET_DETAIL");
                db.AddInParameter(dbCommand, "@IP_intTicketID", DbType.Int32, intTicketID);
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.TicketId = intTicketID;
                    obj.TicketNo = ds.Tables[0].Rows[0][1].ToString();
                    obj.TicketDate = ds.Tables[0].Rows[0][2].ToString();
                    obj.IssueBy = ds.Tables[0].Rows[0][3].ToString();
                    obj.Department = ds.Tables[0].Rows[0][4].ToString();
                    obj.Category = ds.Tables[0].Rows[0][5].ToString();
                    obj.SubCategory = ds.Tables[0].Rows[0][6].ToString();
                    obj.Feedback = ds.Tables[0].Rows[0][7].ToString();
                    obj.Status = ds.Tables[0].Rows[0][8].ToString();
                    obj.Attachment = ds.Tables[0].Rows[0][9].ToString();
                    obj.Action = ds.Tables[0].Rows[0][10].ToString();
                    obj.Remark = ds.Tables[0].Rows[0][11].ToString();
                    obj.TicketTransfer = ds.Tables[0].Rows[0][12].ToString();
                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Ticket.cs", "TrackTicketDetail", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return obj;
        }
    }
}
