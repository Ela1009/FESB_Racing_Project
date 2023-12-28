using System;
using System.Windows;
using System.Windows.Threading;

namespace StopwatchApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            UpdateElapsedTime();
        }

        private void UpdateElapsedTime()
        {
            stopwatchText.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            elapsedTime = TimeSpan.Zero;
            UpdateElapsedTime();
        }
    }
}
