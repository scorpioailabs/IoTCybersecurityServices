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
    public class DeviceData
    {
        private IConfiguration _config;

        public DeviceData(IConfiguration config)
        {
            _config = config;
        }

        public List<DeviceModel> GetDevices()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var output = sql.LoadData<DeviceModel, dynamic>("dbo.spDeviceLookup", new { }, "IoTCybersecurityDb");
            return output;
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; " +
                "Initial Catalog=IoTCybersecurityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (SqlCommand cmd = new SqlCommand("dbo.spDeleteDeviceLookup", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.NVarChar);
                cmd.Parameters["@PrimaryKey"].Value = id;

                // open connection, execute command, close connection
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }            
        }

        public void Add(DeviceModel device)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; " +
                "Initial Catalog=IoTCybersecurityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (SqlCommand cmd = new SqlCommand("dbo.spAddDeviceLookup", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("hostname", device.Hostname);
                cmd.Parameters.AddWithValue("ipaddress", device.IpAddress);
                cmd.Parameters.AddWithValue("macaddress", device.MACAddress);
                cmd.Parameters.AddWithValue("vendor", device.Vendor);
                cmd.Parameters.AddWithValue("userid", device.UserId);
                cmd.Parameters.AddWithValue("devicedescription", device.DeviceDescription);
                // open connection, execute command, close connection
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Edit(DeviceModel device)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; " +
                "Initial Catalog=IoTCybersecurityDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (SqlCommand cmd = new SqlCommand("dbo.spEditDeviceLookup", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.NVarChar);
                cmd.Parameters["@PrimaryKey"].Value = device.Id;
                cmd.Parameters.AddWithValue("devicedescription", device.DeviceDescription);
                // open connection, execute command, close connection
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
