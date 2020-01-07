using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTCybersecurityAPI.Library.Models
{
    public class DeviceModel
    {
        public int Id { get; set; }

        public virtual string UserId { get; set; }

        public string DeviceName { get; set; }

        public string UniqueIdentifier { get; set; }

        public string DeviceDescription { get; set; }

        public string Hostname { get; set; }

        public string Vendor { get; set; }

        public string MACAddress { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime? LastSeen { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string Status { get; set; }

        public string IpAddress { get; set; }
    }
}
