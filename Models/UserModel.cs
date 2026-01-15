using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickingAppModel.Models
{
    public class UserModel : CommonModel
    {
        [Required(ErrorMessage = "Please Enter First Name"), MaxLength(10)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [MaxLength(10)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile No.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Display(Name = "Mobile No")]
        [MaxLength(10)]

        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please Enter Email"), MaxLength(50)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(25)]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Please Select Department")]
        [Display(Name = "Department")]
        public int DeptId { get; set; }
        public List<DeptModel> DeptList { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Select Role")]
        [Display(Name = "Role")]
        public int RoleId { get; set; }
    }
    public class UserModelList : CommonModel
    {
        public int UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Name { get; set; }
        [Display(Name = "Department")]
        public string DeptName { get; set; }
       
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Status { get; set; } 
        public List<UserModelList> UserList { get; set; }
       
    }

    public class UserChangePassword :CommonModel
    {
        [Required(ErrorMessage = "Please Enter Current Password")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }


        [Required(ErrorMessage = "Please Enter New Password")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
    public class DDLUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
