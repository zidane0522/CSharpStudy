using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordObjLearning
{
    public class BHFS_Apply_MainContent
    {
        public ApplicantInfo ApplicnatInfo { get; set; }

        public ProxyInfo ProxyInfo { get; set; }

        public TmInfo TmInfo { get; set; }

        public string ReferLine { get; set; }//法律依据

        public string Explaination{ get; set; }//事实与理由

        public DateTime CreateDate { get; set; }
    }
}
