using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SSHClient.Models;
using SSHClient.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace SSHClient
{
    public sealed partial class MainWindow : Window
    {
        private List<Device> Devices = new();

        /// <summary>
        /// Initializes a new instance of the MainWindow class and configures the main application window.
        /// </summary>
        /// <remarks>This constructor sets the initial size and title of the application window.
        /// Additional initialization logic, such as loading dummy devices for debugging, is also performed.</remarks>
        public MainWindow()
        {
            this.InitializeComponent();
            LoggingService.Info("MainWindow initialized");

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
            appWindow.Title = "Main Menu | SSH Client";

            // DEBUG: Load dummy devices
            LoadDummyDevices();
        }

        // - - - - - - - - - -
        // DEBUG SECTION
        // TODO: COMMENT OR DELETE UPON RELEASE
        // - - - - - - - - - -

        private void LoadDummyDevices()
        {
            DevicesPanel.Children.Clear();

            AddDeviceCard("Raspberry Pi (Blue)", "superadmin@192.168.1.24");
            AddDeviceCard("Raspberry Pi (Red)", "superadmin@192.168.1.24");
            AddDeviceCard("Personal Hub Heroku", "superadmin@192.168.1.24");
        }

        private void AddDeviceCard(string nickname, string connection)
        {
            // Outer card
            var card = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(12),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            // Layout inside the card
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });  
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });  // Connect button
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });  // Padding
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });  // Settings button

            // Left side text
            var textPanel = new StackPanel();

            textPanel.Children.Add(new TextBlock
            {
                Text = nickname,
                FontSize = 16,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });

            textPanel.Children.Add(new TextBlock
            {
                Text = connection,
                Opacity = 0.7
            });

            Grid.SetColumn(textPanel, 0);
            grid.Children.Add(textPanel);

            // Connect button
            var connectButton = new Button
            {
                Content = "Connect",
                Height = 32,
                MinWidth = 90
            };

            Grid.SetColumn(connectButton, 1);
            grid.Children.Add(connectButton);

            // Settings button
            var settingsButton = new Button
            {
                Content = "Settings",
                Height = 32,
                MinWidth = 90
            };

            Grid.SetColumn(settingsButton, 3);
            grid.Children.Add(settingsButton);

            card.Child = grid;

            DevicesPanel.Children.Add(card);
        }

        // - - - - - - - - - -
        // Events
        // - - - - - - - - - -
        /// <summary>
        /// Handles the click event for adding a new device by displaying a dialog and, if confirmed, adding the device
        /// to the collection.
        /// </summary>
        /// <remarks>This method displays a dialog for entering device information. If the user confirms
        /// the dialog, the new device is added to the collection and the input fields are cleared. This method is
        /// intended to be used as an event handler for a UI element's click event.</remarks>
        /// <param name="sender">The source of the event, typically the button that was clicked.</param>
        /// <param name="e">The event data associated with the click event.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private void OpenAddDeviceWindow_Click(object sender, RoutedEventArgs e)
        {
            var openAddDeviceWindow = new AddDeviceWindow();
            openAddDeviceWindow.Activate();
        }

        // - - - - - - - - - -
        // Window methods
        // - - - - - - - - - -
        private void RenderDeviceCards()
        {
            DevicesPanel.Children.Clear();

            foreach (var device in Devices)
            {
                var card = new Grid
                {
                    Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Colors.Purple),
                    Margin = new Thickness(4),
                };

                card.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                card.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });

                var infoStack = new StackPanel();
                infoStack.Children.Add(new TextBlock { Text = device.Nickname, FontSize = 18, Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Colors.White), FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
                infoStack.Children.Add(new TextBlock { Text = device.Username, FontSize = 14, Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Colors.White) });
                infoStack.Children.Add(new TextBlock { Text = device.IPAddress, FontSize = 14, Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Gray) });
                Grid.SetColumn(infoStack, 0);
                card.Children.Add(infoStack);

                // Right: buttons
                var buttonStack = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
                var settingsButton = new Button { Content = "⚙" };
                settingsButton.Click += (s, e) => EditDevice(device);
                var connectButton = new Button { Content = "Connect" };
                //connectButton.Click += (s, e) => ConnectDevice(device);
                buttonStack.Children.Add(settingsButton);
                buttonStack.Children.Add(connectButton);
                Grid.SetColumn(buttonStack, 1);
                card.Children.Add(buttonStack);

                DevicesPanel.Children.Add(card);
            }
        }

        private void RenderDevices()
        {
            DevicesPanel.Children.Clear();

            foreach (var device in Devices)
            {
                //AddDeviceCard(device);
            }
        }
        // - - - - - - - - - -
        // Device methods
        // - - - - - - - - - -
        //private static void ConnectDevice(Device device)
        //{
        //    // Retrieve password from Windows Credential Manager
        //    string? password = CredentialService.GetPassword(device.CredentialKey);

        //    // Launch Windows Terminal SSH command
        //    string args = $"ssh {device.Username}@{device.IPAddress}";
        //    // Optional: handle password prompt if needed

        //    Process.Start(new ProcessStartInfo
        //    {
        //        FileName = "wt.exe",
        //        Arguments = args,
        //        UseShellExecute = true
        //    });
        //}

        private void EditDevice(Device device)
        {
            // TODO: Implement settings dialog to edit device properties and password
            // For now, just show a simple message
            var dialog = new ContentDialog
            {
                Title = $"Edit {device.Nickname}",
                Content = "Here you will edit username, IP address, nickname, certificate, and password.",
                CloseButtonText = "Close"
            };
            _ = dialog.ShowAsync();
        }
    }
}