using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickingAppModel.Models
{
    public class NavbarModel
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string URL { get; set; }
        public int ParentModuleId { get; set; }
        public string Iconimg { get; set; }
        public int Status { get; set; }
        public List<NavbarModel> NavbarList { get; set; }
    }
}
