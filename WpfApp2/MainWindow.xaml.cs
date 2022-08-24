using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LauncherHandler.Instance.StartUp();
            
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            // ReSharper disable once HeapView.DelegateAllocation
            timer.Tick += CheckInternetConnection;
            timer.Start();
        }

        private void CheckInternetConnection(object? sender, EventArgs e)
        {
            /*Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ErrorMessage.Text = DateTime.Now.ToString("hh:mm:ss t z");   
            });*/
            InternetError.Visibility = InternetAvailability.IsInternetAvailable() ? Visibility.Collapsed : Visibility.Visible;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LauncherHandler.Instance.TryStart();
                Error.Visibility = Visibility.Collapsed;
            }
            catch
            {
                Error.Text = "files are corrupted";
                Error.Visibility = Visibility.Visible;
            }
        }

        private void Up_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}