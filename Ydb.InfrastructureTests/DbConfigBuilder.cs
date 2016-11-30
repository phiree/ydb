using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.InfrastructureTests
{
   public class DbConfigBuilder
    {
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
        public DbConfigBuilder ReplaceDianzhuDb(string newDb)
        {
            DbList[0] = newDb;
            return this;
        }
        
        private string[] DbList = new string[] { "dianzhu", "ydb_finance", "ydb_instantmessage", "ydb_businessresource", "ydb_membership", "ydb_common" };

    }
}
