using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using IoTCybersecurityAPI.Library.Models;
using IoTCybersecurityAPI.Library.DataAccess;

namespace IoTCybersecurityApiREST.Controllers
{
    [Produces("application/json")]
    [Route("api/device")]
    [ApiVersion("1.0")]
    //[Authorize]
    [AllowAnonymous]
    public class DeviceController : ControllerBase
    {
        private readonly IConfiguration _config;
        public DeviceController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/values
        [HttpGet]
        public List<DeviceModel> Get()
        {
            DeviceData data = new DeviceData(_config);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deviceList = data.GetDevices();
            var myDevices = deviceList?.FindAll(p => p.UserId.Equals(currentUserId));
            return myDevices;
        }

        [HttpDelete, Route("Remove")]
        public void Remove(int Id)
        {
            DeviceData data = new DeviceData(_config);
            var device = data.GetDevices()?.FindAll(p => p.Id.Equals(Id));
            var myDevice = device.FirstOrDefault();
            data.Delete(myDevice.Id);
        }

        // POST api/Device/Add
        [HttpPost, Route("Add")]
        public async Task Add([FromBody]DeviceModel model)
        {
            DeviceData data = new DeviceData(_config);

            var device = new DeviceModel()
            {
                Hostname = model.Hostname,
                IpAddress = model.IpAddress,
                MACAddress = model.MACAddress,
                Vendor = model.Vendor,
                UserId = model.UserId,
                DeviceDescription = model.DeviceDescription
            };
            data.Add(model);
        }

        //POST api/Device/Edit
        [HttpPost, Route("Edit")]
        public void Edit(DeviceModel vm)
        {
            DeviceData data = new DeviceData(_config);
            var updatedDevice = new DeviceModel()
            {
                Id = vm.Id,
                Hostname = vm.Hostname,
                IpAddress = vm.IpAddress,
                MACAddress = vm.MACAddress,
                Vendor = vm.Vendor,
                UserId = vm.UserId,
                DeviceDescription = vm.DeviceDescription
            };
            data.Edit(updatedDevice);
        }
    }
}