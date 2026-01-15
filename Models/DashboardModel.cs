using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TickingAppModel.Models
{
    public class DashboardModel
    {
        public int OpenTicket { get; set; }
        public int ClosedTicket { get; set; }
        public int TotalUser { get; set; }
        public int OverDue { get; set; }

        public List<object> DougnutchartData = new List<object>();

        public List<object> BarchartData = new List<object>();

    }
    public class AgentModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
        [Display(Name = "Pending Ticket")]
        public int Pending { get; set; }
        [Display(Name = "Closed Ticket")]
        public int Closed { get; set; }
        public List<AgentModel> AgentList { get; set; }
    }
}
