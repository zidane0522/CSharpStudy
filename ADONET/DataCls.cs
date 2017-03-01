using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ADONET
{
    public class DataCls
    {
        public DataCls(string databaseName)
        {
            //string source = "server=(local);integrated security=SSPI;database=Northwind";
            //conn = new SqlConnection(source);

            //conn.Open();
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[databaseName];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
            using (TransactionScope scope=new TransactionScope(TransactionScopeOption.Required))
            {
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.Open();

                    //Do Sql Jobs

                    //sqlStr="select Contactname"
                    scope.Complete();//将事务显式标记为完成，如果不调用这个方法，事务就会回滚，以便不对数据库进行任何修改
                }
            }
        }

        private void getCmd()
        {
            string source = "server=(local); integrated security=SSPI;database=Northwind";
            string select = "select * from Customers";
            SqlConnection conn = new SqlConnection(source);
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
        }

        public SqlConnection conn { get; set; }

        public string sqlStr { get; set; }

        public SqlCommand cmd { get; set; }

        public void close()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}
