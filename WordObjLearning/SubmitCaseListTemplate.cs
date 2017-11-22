using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordObjLearning
{
    /// <summary>
    /// 报送商标评审委员会案件清单
    /// </summary>
    public class SubmitCaseListTemplate
    {
        public List<SubmitCaseInfo> SubmitCaseList { get; set; }

        public DateTime CreateDate { get; set; }

        public ProxyInfo ProxyInfo { get; set; }

        public string Remark { get; set; }//备注

        public string PaperReceiveBiz { get; set; }//收文员
    }

    public struct SubmitCaseInfo
    {
        public TmInfo TmInfo { get; set; }

        public ApplicantInfo ApplicantInfo { get; set; }

        public string CaseType { get; set; }//案件类型

        public string PaperType { get; set; }//材料种类（首次申请）

        public string Remark { get; set; }//备注
    }

    public struct TmInfo
    {
        public string RegNum { get; set; }

        public string TmName { get; set; }

        public string Ictm { get; set; }

        public string SendFileNum { get; set; }//商标局发文号
    }

    public struct ApplicantInfo
    {
        public string Applicant { get; set; }

        public string Nation { get; set; }

        public string ApplicantAddress { get; set; }//通信地址

        public string LegalBiz { get; set; }//法定代表人，责任人

        public string Duties { get; set; }//职务
    }

    public struct ProxyInfo
    {
        public string PorxyOrg { get; set; }//代理组织

        public string ProxyBiz { get; set; }//代理人

        public string PostCode { get; set; }//邮编

        public string Phone { get; set; }//电话

        public string Fax { get; set; }//传真

        public string Address { get; set; }
    }
}
