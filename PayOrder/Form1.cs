using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetCodeBtn_Click(object sender, EventArgs e)
        {

        }

        private void GetZfb()
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", "app_id", "merchant_private_key", "json", "RSA", "alipay_public_key", "GBK");
            AlipayTradePayRequest request = new AlipayTradePayRequest();
            request.BizContent = "{" +
            "    \"out_trade_no\":\"20150320010101001\"," +
            "    \"scene\":\"bar_code,wave_code\"," +
            "    \"auth_code\":\"28763443825664394\"," +
            "    \"subject\":\"Iphone6 16G\"," +
            "    \"seller_id\":\"2088102146225135\"," +
            "    \"total_amount\":88.88," +
            "    \"discountable_amount\":8.88," +
            "    \"undiscountable_amount\":80.00," +
            "    \"body\":\"Iphone6 16G\"," +
            "      \"goods_detail\":[{" +
            "                \"goods_id\":\"apple-01\"," +
            "        \"alipay_goods_id\":\"20010001\"," +
            "        \"goods_name\":\"ipad\"," +
            "        \"quantity\":1," +
            "        \"price\":2000," +
            "        \"goods_category\":\"34543238\"," +
            "        \"body\":\"特价手机\"," +
            "        \"show_url\":\"http://www.alipay.com/xxx.jpg\"" +
            "        }]," +
            "    \"operator_id\":\"yx_001\"," +
            "    \"store_id\":\"NJ_001\"," +
            "    \"terminal_id\":\"NJ_T_001\"," +
            "    \"alipay_store_id\":\"2016041400077000000003314986\"," +
            "    \"extend_params\":{" +
            "      \"sys_service_provider_id\":\"2088511833207846\"," +
            "      \"hb_fq_num\":\"3\"," +
            "      \"hb_fq_seller_percent\":\"100\"" +
            "    }," +
            "    \"timeout_express\":\"90m\"," +
            "    \"royalty_info\":{" +
            "      \"royalty_type\":\"ROYALTY\"," +
            "        \"royalty_detail_infos\":[{" +
            "                    \"serial_no\":1," +
            "          \"trans_in_type\":\"userId\"," +
            "          \"batch_no\":\"123\"," +
            "          \"out_relation_id\":\"20131124001\"," +
            "          \"trans_out_type\":\"userId\"," +
            "          \"trans_out\":\"2088101126765726\"," +
            "          \"trans_in\":\"2088101126708402\"," +
            "          \"amount\":0.1," +
            "          \"desc\":\"分账测试1\"," +
            "          \"amount_percentage\":\"100\"" +
            "          }]" +
            "    }," +
            "    \"sub_merchant\":{" +
            "      \"merchant_id\":\"19023454\"" +
            "    }" +
            "  }";
            AlipayTradePayResponse response = client.Execute(request);
            if (!response.IsError)
            {
                Console.WriteLine("调用成功");
            }
            else
            {
                Console.WriteLine("调用失败");
            }
        }
    }
}
