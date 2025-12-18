using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;

namespace SSHClient
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Set window size
            var handle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));  // Set width and height of the window
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Launch Windows Terminal with SSH
            var startInfo = new ProcessStartInfo
            {
                FileName = "wt.exe",
                Arguments = "ssh user@host",
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }
    }
}