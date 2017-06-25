using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReportFrame.Models
{
    public class Tm_loc_info
    {
        public string TmName { get; set; }

        public string Applicant { get; set; }

        public string Phone { get; set; }

        public string TmIctm { get; set; }

        public string TmItem { get; set; }
    }

    public class TmGroupInfo
    {
        public string GroupID { get; set; }

        public List<string> ItemNameList { get; set; }
    }
}
