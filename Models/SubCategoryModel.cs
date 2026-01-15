using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TickingAppModel.Models
{
    public class SubCategoryModel
    {
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please Enter Sub Category Name")]
        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }
        public List<SubCategoryModel> SubCategoryList { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
    }
}
