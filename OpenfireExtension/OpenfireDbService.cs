using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace OpenfireExtension
{
    /// <summary>
    /// 直接操作openfire数据库
    /// </summary>
    public interface IOpenfireDbService
    {
        /// <summary>
        /// 个别用户
        /// </summary>
        /// <param name="userids"></param>
        /// <param name="groupname"></param>
        void AddUsersToGroup(string userids, string groupname);
        IList<string> GetUsersInGroup(string groupName);

    }

    public class OpenfireDbService : IOpenfireDbService
    {
        private Ydb.Common.Infrastructure.IEncryptService encryptService;

        public OpenfireDbService(Ydb.Common.Infrastructure.IEncryptService encryptService)
        {
            this.encryptService = encryptService;
        }

        
        public void AddUsersToGroup(string userids, string groupname)
        {
            string connectionString = encryptService.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["openfire"].ConnectionString, false);
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("AddUserToGroup");
                comm.Connection = conn;
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("addedGroupName", groupname);
                comm.Parameters.AddWithValue("userids", userids);
                comm.ExecuteNonQuery();
            }
        }
        public   IList<string> GetUsersInGroup(string groupName)
        {
            IList<string> userNames = new List<string>();
            string connectionString = encryptService.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["openfire"].ConnectionString, false);
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("select username from ofgroupuser where groupname='"+groupName+"'");
                comm.Connection = conn;
                
               MySqlDataReader reader=  comm.ExecuteReader();
                while (reader.Read())
                {
                    string userName = (string)reader["username"];
                    userNames.Add(userName);
                }
                reader.Close();
            }
            return userNames;
        }
    }
}