using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TickingAppModel.Models
{
    //public class CommonClass
    //{
    //    public int intUserID { get; set; }
    //    public string strMachineIP { get; set; }
    //    public string strException { get; set; }
    //}
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        public string strUserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string strPassword { get; set; }
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; }
    }
}
