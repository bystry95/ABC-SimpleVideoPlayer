using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_MEDIAPLAYER
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public static double iconSize = 28;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VideoClipPauseHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Pause();
            if (timer != null)
                timer.Stop();
        }

        private void VideoClipMuteHandler(object sender, RoutedEventArgs e)
        {
            if (videoClip.Volume == 0)
                videoClip.Volume = 1;
            else
                videoClip.Volume = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) //Załadowanie podglądu filmu.
        {
            videoClip.ScrubbingEnabled = true;
            videoClip.Stop();
        }

        private void videoClip_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message, "Error occurred", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void videoClip_MediaOpened(object sender, RoutedEventArgs e)
        {
            

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            slider_time.Maximum = videoClip.NaturalDuration.TimeSpan.TotalSeconds;
            label_totalTime.Content = videoClip.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            
            videoClip.Play();
            if (timer != null)
                timer.Start();
            //videoClip.Pause();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            slider_time.Value = videoClip.Position.TotalSeconds;
        }

        private void slider_time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoClip.Position = TimeSpan.FromSeconds(slider_time.Value);
        }

        private void slider_time_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            videoClip.Pause();
            if (timer != null)
                timer.Stop();
        }

        private void slider_time_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            videoClip.Play();
            if (timer != null)
                timer.Start();
        }

        private void MediaElementCheckedHandler(object sender, RoutedEventArgs e)
        {
            Window main = Application.Current.Windows[0];
            main.WindowStyle = WindowStyle.None;
            TopScreenMenu.Visibility = Visibility.Collapsed;
            BottomScreenMenu.Visibility = Visibility.Collapsed;
            main.WindowState = WindowState.Maximized;

        }

        private void MediaElementUncheckedHandler(object sender, RoutedEventArgs e)
        {
            Window main = Application.Current.Windows[0];
            main.WindowStyle = WindowStyle.ThreeDBorderWindow;
            TopScreenMenu.Visibility = Visibility.Visible;
            BottomScreenMenu.Visibility = Visibility.Visible;
            main.WindowState = WindowState.Normal;
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void CustomCommand_CanExecuted_Always(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CustomCommand_Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (timer != null) { 
                if (!timer.IsEnabled)
                {
                    videoClip.Play();
                    if (timer != null)
                        timer.Start();
                }
                else if (timer.IsEnabled)
                {
                    videoClip.Pause();
                    if (timer != null)
                        timer.Stop();
                }
            }
        }

        private void CustomCommand_Fullscreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window main = Application.Current.Windows[0];
            if (main.WindowState == WindowState.Maximized)
            {
                main.WindowStyle = WindowStyle.ThreeDBorderWindow;
                TopScreenMenu.Visibility = Visibility.Visible;
                BottomScreenMenu.Visibility = Visibility.Visible;
                main.WindowState = WindowState.Normal;
                main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                main.WindowStyle = WindowStyle.None;
                TopScreenMenu.Visibility = Visibility.Collapsed;
                BottomScreenMenu.Visibility = Visibility.Collapsed;
                main.WindowState = WindowState.Maximized;
            }
        }

        private void CommandBinding_Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Uri filepath = null;
            ofd.Filter = "Video files|*.mp4;*.flv;*.avi;*.mov";
            ofd.ShowDialog();

            if (ofd.FileName != null && ofd.FileName != "")
                filepath = new Uri(ofd.FileName);

            videoClip.Source = filepath;
            Application.Current.MainWindow.Focus();
            videoClip.Play();
        }

        private void CommandBinding_Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CommandBinding_Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            videoClip.Close();
        }

        private void CommandBinding_Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private void videoClip_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoClip.Stop();
            if (timer != null)
                timer.Stop();
        }

        private void CommandBinding_CanExecuted_FileLoaded(object sender, CanExecuteRoutedEventArgs e)
        {
            if (videoClip.IsLoaded && videoClip.Source != null)
                e.CanExecute = true;
        }

        private void CustomCommand_Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            videoClip.Stop();
            if (timer != null)
                timer.Stop();
        }

        private void CommandBinding_VideoClip10Forward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            videoClip.Position += TimeSpan.FromSeconds(10);
        }

        private void CommandBinding_VideoClip10Backward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            videoClip.Position -= TimeSpan.FromSeconds(10);
        }
    }
}
