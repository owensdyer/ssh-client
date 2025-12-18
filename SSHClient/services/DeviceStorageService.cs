using SSHClient.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SSHClient.Services
{
    public static class DeviceStorageService
    {
        private static string FilePath =>
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "devices.json");

        public static List<Device> LoadDevices()
        {
            if (!File.Exists(FilePath)) return new List<Device>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Device>>(json);
        }

        public static void SaveDevices(List<Device> devices)
        {
            var folder = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            var json = JsonSerializer.Serialize(devices, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}