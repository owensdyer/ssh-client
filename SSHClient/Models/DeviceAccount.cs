using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSHClient.Models
{
    /// <summary>
    /// Represents the credentials and certificate information for a device account.
    /// </summary>
    public class DeviceAccount
    {
        public required string username { get; set; }
        public string? password { get; set; }
        public string? CertificatePath { get; set; }
    }
}
