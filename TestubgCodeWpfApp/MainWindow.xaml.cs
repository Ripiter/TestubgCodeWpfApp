using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TestubgCodeWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float hours = 0;
        private float minutes = 0;
        private float seconds = 0;
        private bool canClick = true;
        private string selectedItem;

        DispatcherTimer dispatcherTimer;
        Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            // Hides custom input
            CustomText.Visibility = Visibility.Hidden;

            // Startig values of the label
            Hour.Content = "0";
            Minute.Content = "0";
            Second.Content = "0";

            string[] boy = new string[] { "Shutdown", "Custom" };

            cmbColors.ItemsSource = boy;

            cmbColors.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        ///  Drop down menu event 
        ///  Whenever selected values changes this method will be called 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = cmbColors.SelectedItem.ToString();

            // Check if custom is selected
            if (selectedItem == "Custom")
                CustomText.Visibility = Visibility.Visible;
            else
                CustomText.Visibility = Visibility.Hidden;

        }

        #region Buttons
        private void AddHour(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;
            hours++;
            Hour.Content = hours;
        }

        private void MinusHour(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;

            if (hours <= 0)
                return;

            hours--;
            Hour.Content = hours;
        }

        private void AddSecond(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;

            seconds++;
            Second.Content = seconds;
        }

        private void MinusSecond(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;

            if (seconds <= 0)
                return;

            seconds--;
            Second.Content = seconds;
        }

        private void AddMinute(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;

            minutes++;
            Minute.Content = minutes;
        }

        private void MinusMinute(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;

            if (minutes <= 0)
                return;

            minutes--;
            Minute.Content = minutes;
        }

        /// <summary>
        /// Starts the timer and send values of hour minutes and secodns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (canClick == false)
                return;

            timer = new Timer(hours, minutes, seconds);

            UpdateSeconds();
            StartMethod();
            canClick = false;
        }

        /// <summary>
        /// Pauses the timer coutdown and stops the shutdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pause(object sender, RoutedEventArgs e)
        {
            Reset();
            StopShutDown();
        }
        #endregion

        #region Timer

        /// <summary>
        /// Creates event that is called every second
        /// </summary>
        private void UpdateSeconds()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateContentSecond);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void StopTimer()
        {
            try
            {
                dispatcherTimer.Stop();
            }
            catch
            {

            }
        }

        private void UpdateContentHour()
        {
            timer.Hour--;
            Hour.Content = timer.ReturnHours();
        }

        private void UpdateContentMinute()
        {
            float temp = timer.ReturnMinutes();

            if (temp <= 0)
            {
                if (timer.Hour > 0)
                {
                    timer.Minute--;
                    UpdateContentHour();
                    timer.Minute = 59f;
                }
                Second.Content = 60f;
            }
            else
                timer.Minute--;

            Minute.Content = timer.ReturnMinutes();
        }

        private void UpdateContentSecond(object sender, EventArgs e)
        {
            float temp = timer.ReturnSeconds();

            if (timer.Second == 0 && timer.Minute == 0 && timer.Hour == 0)
                Done();

            if (temp <= 0)
            {
                if (timer.Minute >= 0)
                {
                    UpdateContentMinute();
                    timer.Second = 60f;
                }
            }

            Second.Content = temp;
        }

        #endregion

        /// <summary>
        /// Executes when time times is finished
        /// </summary>
        private void Done()
        {
            if (selectedItem == "Custom")
                MessageBox.Show(customInput, "It's Show Time", MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.OK,MessageBoxOptions.ServiceNotification);
            Reset();
        }

        /// <summary>
        /// Sets all values to 0 and resets the timer
        /// </summary>
        private void Reset()
        {
            StopTimer();

            Hour.Content = 0;
            hours = 0;

            Minute.Content = 0;
            minutes = 0;

            Second.Content = 0;
            seconds = 0;

            canClick = true;
        }

        /// <summary>
        /// Selects which method should run
        /// </summary>
        void StartMethod()
        {
            switch (selectedItem)
            {
                case "Shutdown":
                    ShutDown(SecondsBack());
                    break;
                default:
                    break;
            }
        }

        #region Shutdown Methods
        /// <summary>
        /// Shutsdown the computer after x time
        /// </summary>
        /// <param name="time"></param>
        private void ShutDown(string time)
        {
            MessageBox.Show(time + " sec to shutdown");
            timer.ShutDown(time);
        }

        private void StopShutDown()
        {
            try
            {
                if (selectedItem == "Shutdown")
                {
                    MessageBox.Show("Shutdown stopped");
                    timer.StopShutdown();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Custom Alert
        string customInput;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // customInput = CustomText.Text;
        }
        #endregion

        /// <summary>
        /// Return time that was set in the app in seconds
        /// </summary>
        /// <returns></returns>
        private string SecondsBack()
        {
            float sec = 0;
            sec = sec + (hours * 60) * 60;
            sec = sec + (minutes * 60);
            sec = sec + seconds;

            return sec.ToString();
        }

        private void CustomText_TextChanged(object sender, TextChangedEventArgs e)
        {
            customInput = CustomText.Text;
        }
    }
}
