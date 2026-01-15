using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickingAppModel.Models
{
    public class RoleListingDT
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public List<RoleListingDT> RoleList { get; set; }
    }
    public class RoleCreation
    {
        public int RoleID { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string RoleName { get; set; }
        public string Status { get; set; }
        [Required]
        public string ModuleIds { get; set; }

    }
}