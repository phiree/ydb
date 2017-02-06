using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InfrastructureTests
{
   public class DbConfigBuilder
    {
        Ydb.Infrastructure.EncryptService encryptService = new Infrastructure.EncryptService();
        IList<string> configResult;
        public IList<string> BuildForServer(string host, string uid, string pwd)
        {
            return BuildForServer(host, uid, pwd, "3306");
        }
        public IList<string> BuildForServer(string host, string uid, string pwd, string port)
        {
            return DbList.Select(x => BuildOneDb(host, x, uid, pwd, port)).ToList();
        }
        private string BuildOneDb(string s, string d, string u, string w, string p)
        {

            string one = string.Format("{0}_{1}___data source={0};database={1};uid={2};pwd={3};port={4};", s, d, u, w, p);
            return one;
        }
        public IList<string> BuildForServerConfig(string host, string uid, string pwd, string port)
        {
            return DbList.Select((x,index) => BuildOneDbConfig(host, x, uid, pwd, port,DbconfigKeys[index])).ToList();
        }
        private string BuildOneDbConfig(string s, string d, string u, string w, string p,string key)
        {

            string conn = BuildOneDb(s, d, u, w, p);
            string encrypted = encryptService.Encrypt(conn,false);
            string config = string.Format("<add name=\"{0}\"  connectionString=\"{1}\"/>",
                key,encrypted);
            return config;
            
        }
        public DbConfigBuilder ReplaceDianzhuDb(string newDb)
        {
            DbList[0] = newDb;
            return this;
        }

        private string[] DbList = new string[] { "dianzhu", "ydb_finance", "ydb_instantmessage",
            "ydb_businessresource", "ydb_membership", "ydb_common", "ydb_order" };

        private string[] DbconfigKeys = new string[] { "DianzhuConnectionString", "ydb_finance", "ydb_instantmessage",
            "ydb_businessresource", "ydb_membership", "ydb_common", "ydb_order" };

    }
}
