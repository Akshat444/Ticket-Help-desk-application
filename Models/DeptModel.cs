using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TickingAppModel.Models
{
    public class DeptModel
    {
        public int DeptId { get; set; }
        [Required(ErrorMessage = "Please Enter Department Name")]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
        public string Status { get; set; }
        public List<DeptModel> DeptList { get; set; }
    }
}
