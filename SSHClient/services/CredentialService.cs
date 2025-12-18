using Windows.Security.Credentials;

namespace SSHClient.Services
{
    public static class CredentialService
    {
        // Resource name groups credentials
        private static readonly string ResourceName = "SSHClient";

        /// <summary>
        /// Save a password for a device in Windows Credential Manager
        /// </summary>
        /// <param name="key">Unique key for the device</param>
        /// <param>Device password</param>
        public static void SavePassword(string key, string password)
        {
            var vault = new PasswordVault();

            // Remove existing credential if it exists
            try
            {
                var existingVault = vault.Retrieve(ResourceName, key);
                vault.Remove(existingVault);
            }
            catch
            {
                // Credential does not exist, ignore
            }
        }

        /// <summary>
        /// Retrieve a password for a device from Windows Credential Manager
        /// </summary>
        /// <param name="key">Unique key for the device</param>
        /// <returns>Password (string), or null if not found</returns>
        public static string? GetPassword(string key)
        {
            var vault = new PasswordVault();

            try
            {
                var credential = vault.Retrieve(ResourceName, key);
                credential.RetrievePassword();
                return credential.Password;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>Delete a password for a device from Windows Credential Manager</summary>
        /// <param name="key">Unique key for the device</param>
        public static void DeletePassword(string key)
        {
            var vault = new PasswordVault();

            try
            {
                var credential = vault.Retrieve(ResourceName, key);
                vault.Remove(credential);
            }
            catch
            {
                // Credential does not exist, ignore
            }
        }
    }
}