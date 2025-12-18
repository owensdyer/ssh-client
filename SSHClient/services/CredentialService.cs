using CredentialManagement;

namespace SSHClient.services
{
    public static class CredentialService
    {
        // Save password for a device
        public static void SavePassword(string key, string password)
        {
            var cred = new Credential
            {
                Target = key,
                Password = password,
                PersistanceType = PersistanceType.LocalComputer,
                Type = CredentialType.Generic
            };
            cred.Save();
        }

        public static string GetPassword(string key)
        {
            var cred = new Credential { Target = key };
            return cred.Load() ? cred.Password : null;
        }

        public static void DeletePassword(string key)
        {
            var cred = new Credential { Target = key };
            cred.Delete();
        }
    }
}
