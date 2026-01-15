using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TickingAppModel.Models;
namespace TickingAppModel.Repository
{
    public class Dashboard: BaseClass
    {
        Database db = null;
        LogError objLogErr = new LogError();

        public DashboardModel DashboardInfo()
        {
            DashboardModel objList = new DashboardModel();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_DASHBOARD");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand, "@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objList.OpenTicket =Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    objList.ClosedTicket = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                    objList.TotalUser = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
                    objList.OverDue = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString());

                    //Dougnut Chart
                    List<string> DeptName = new List<string>();
                    List<int> DeptCount = new List<int>();
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        DeptName.Add(Convert.ToString(row["DeptName"]));
                        DeptCount.Add(Convert.ToInt32(row["DeptCount"]));
                    }
                    objList.DougnutchartData.Add(DeptName);
                    objList.DougnutchartData.Add(DeptCount);

                    //Bar Chart
                    List<string> labels = new List<string>();
                    List<int> series1 = new List<int>();
                    List<int> series2 = new List<int>();
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        labels.Add(Convert.ToString(row["Date"]));
                        series1.Add(Convert.ToInt32(row["OpenTicket"]));
                        series2.Add(Convert.ToInt32(row["ClosedTicket"]));
                    }
                    objList.BarchartData.Add(labels);
                    objList.BarchartData.Add(series1);
                    objList.BarchartData.Add(series2);

                }
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dashboard.cs", "DashboardInfo", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return objList;
        }
        public List<AgentModel> AgentInfo()
        {
            List<AgentModel> agentModel = new List<AgentModel>();
            try
            {
                db = GetDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("USP_AGENT_STATUS");
                db.AddInParameter(dbCommand, "@IP_intUserID", DbType.Int32, Constant.APP_USER_ID);
                db.AddInParameter(dbCommand, "@IP_strMachineIP", DbType.String, Constant.MACHINEIP);
                db.AddOutParameter(dbCommand,"@OP_strException", DbType.String, 500);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                agentModel = ds.Tables[0].AsEnumerable()
                          .Select(row => new AgentModel
                          {
                              UserName = row.Field<string>(0),
                              DeptName = row.Field<string>(1),
                              Pending = row.Field<int>(2),
                              Closed = row.Field<int>(3),
                          }).ToList();
            }
            catch (Exception ex)
            {
                objLogErr.ApplicationLogErrorInDB(Constant.API_NAME, ex.Message, "Dashboard.cs", "AgentInfo", Constant.APP_USER_ID, Constant.MACHINEIP);
            }
            return agentModel;
        }
    }
}
