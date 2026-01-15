using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickingAppModel.Models
{
    public class TATReportModel
    {
        [Display(Name = "DateTime")]
        public string TDate { get; set; }
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Status { get; set; }
        public String Priority { get; set; }
        public string TicketTransfer { get; set; }
        public string ClosingDate { get; set; }
        public string TAT { get; set; }
        public List<TATReportModel> TicketList { get; set; }
    }
    public class DetailReportModel
    {
        [Display(Name = "Date")]
        public String TDate { get; set; }
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Comments { get; set; }
        public string Attachment { get; set; }
        public string TicketTransfer { get; set; }
        public string Closingdate { get; set; }
        public string ClosedBy { get; set; }
        public string IssuedBy { get; set; }
        public List<DetailReportModel> TicketList { get; set; }
    }
}
