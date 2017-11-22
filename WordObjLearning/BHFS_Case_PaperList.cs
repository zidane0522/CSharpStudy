using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordObjLearning
{
    public class BHFS_Case_PaperList
    {
        public TmInfo TmInfo{ get; set; }

        public List<PaperInfo> PaperInfoList { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public struct PaperInfo
    {
        public string PaperName { get; set; }

        public int PaperCount { get; set; }

        public string PaperIndex { get; set; }

        public string Remark { get; set; }
    }
}
