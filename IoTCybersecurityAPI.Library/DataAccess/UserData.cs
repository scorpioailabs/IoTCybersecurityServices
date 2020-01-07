using IoTCybersecurityAPI.Library.Internal.DataAccess;
using IoTCybersecurityAPI.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTCybersecurityAPI.Library.DataAccess
{
    public class UserData
    {
        private IConfiguration _config;

        public UserData(IConfiguration config)
        {
            _config = config;
        }

        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "IoTCybersecurityDb");

            return output;
        }

        public void Add(UserModel user)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);


            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; " +
                "Initial Catalog=IoTCybersecurityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (SqlCommand cmd = new SqlCommand("dbo.spAddUserLookup", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", user.Id);
                cmd.Parameters.AddWithValue("firstname", user.FirstName);
                cmd.Parameters.AddWithValue("lastname", user.LastName);
                cmd.Parameters.AddWithValue("emailaddress", user.EmailAddress);
                // open connection, execute command, close connection
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
