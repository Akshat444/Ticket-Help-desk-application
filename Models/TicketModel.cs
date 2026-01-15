using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace TickingAppModel.Models
{
    public class TicketModel
    {
        public int TicketId { get; set; }
        [Required(ErrorMessage = "Please Select Helplist")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please Select Issue")]
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please Select Priority")]
        public string Priority { get; set; }
        [Required(ErrorMessage = "Please Select Department")]
        public int DeptId { get; set; }
        [Required(ErrorMessage = "Please Enter Comments")]
        public string Comments { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
        public List<DeptModel> DeptList { get; set; }
        public List<SubCategoryModel> SubCategoryList { get; set; }
    
        public string FileName { get; set; }
    }
    public class TicketModelList
    {
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Status { get; set; }
        public String Priority { get; set; }
        [Display(Name = "Date")]
        public String TDate { get; set; }
        public string Waiting { get; set; }
        public int Attachment { get; set; }
        public List<TicketModelList> TicketList { get; set; }
        public string TicketTransfer { get; set; }
        public string Feedback { get; set; }
    }
    public class ViewTicketModel
    {
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string TicketDate { get; set; }
        public string IssueBy { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Feedback { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Please Select Action")]
        public string Action { get; set; }
        [Required(ErrorMessage = "Please Enter Remark")]
        public string Remark { get; set; }
        public string Attachment { get; set; }

        public string TicketTransfer { get; set; }
    }
    public class TransferTicketModel
    {
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string TicketDate { get; set; }
        public string IssueBy { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Feedback { get; set; }
        public string Status { get; set; }

        [Required(ErrorMessage = "Please Select Department")]
        public int DeptId { get; set; }
        [Required(ErrorMessage = "Please Select User")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Transfer Reason")]
        public string Remark { get; set; }
        public string Attachment { get; set; }
    }
    public class TrackTicketModel
    {
        public int TicketId { get; set; }
        public string TicketNo { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Status { get; set; }
        public String Priority { get; set; }
        [Display(Name = "Date")]
        public String TDate { get; set; }
        public string Waiting { get; set; }
        public int Attachment { get; set; }
        public List<TrackTicketModel> TicketList { get; set; }
        public string TicketTransfer { get; set; }

        public string Comments { get; set; }
    }
}
