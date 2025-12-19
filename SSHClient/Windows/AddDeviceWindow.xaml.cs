using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

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

            // Configure window
            var handle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(handle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32(400, 400));
            appWindow.Title = "SSH Client - Add Device";

            appWindow.Resize(new Windows.Graphics.SizeInt32(400, 300));

            var overlappedPresenter = appWindow.Presenter as OverlappedPresenter;
            if (overlappedPresenter != null)
            {
                overlappedPresenter.IsResizable = false; // Prevent resizing
            }
        }
    }
}
