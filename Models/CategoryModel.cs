using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TickingAppModel.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Please Select Department")]
        public int DeptId { get; set; }
 
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
        public List<DeptModel> DeptList { get; set; }

    }
}
