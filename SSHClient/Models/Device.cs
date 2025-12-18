// - - - - -
// Device.cs
// Models the SSH device with necessary connection details.
// - - - - -

namespace SSHClient.Models
{
    public class Device
    {
        public required string Nickname { get; set; }
        public required string IPAddress { get; set; }
        public required string Username { get; set; }
        public string Password { get; set; }
        public string CertificatePath { get; set; }

        // The key used in Credential Manager
        public string CredentialKey => $"{Username}@{IPAddress}";
    }
}
