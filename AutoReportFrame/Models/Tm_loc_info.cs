using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReportFrame.Models
{
    /// <summary>
    /// 一标一类信息类
    /// </summary>
    public class Tm_loc_info
    {
        public string TmName { get; set; }

        public string Applicant { get; set; }

        public string Phone { get; set; }

        public int TmIctm { get; set; }

        public string TmId { get; set; }

        public string TmNum { get; set; }
    }

    /// <summary>
    /// 群组信息类
    /// </summary>
    public class GroupInfo
    {
        public string GroupId { get; set; }

        public List<string> ItemList { get; set; }
    }
}
