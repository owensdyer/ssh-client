// - - - - -
// Device.cs
// Models the SSH device with necessary connection details.
// - - - - -

using System.Collections.Generic;

namespace SSHClient.Models
{
    public class Device
    {
        public required string Nickname { get; set; }
        public required string IPAddress { get; set; }
        public string? CertificatePath { get; set; }
        public List<DeviceAccount> Accounts { get; set; } = new();
    }
}
