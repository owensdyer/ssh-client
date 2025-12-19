using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using SSHClient.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SSHClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDeviceWindow : Window
    {
        public AddDeviceWindow()
        {
            InitializeComponent();
            LoggingService.Info("AddDeviceWindow initialized");

            // Set window size
            var handle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(handle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));

            var overlappedPresenter = appWindow.Presenter as OverlappedPresenter;
            if (overlappedPresenter != null)
            {
                overlappedPresenter.IsResizable = false; // Prevent resizing
            }

            appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));
            appWindow.Title = "Device Configuration | SSH Client";
        }
    }
}
