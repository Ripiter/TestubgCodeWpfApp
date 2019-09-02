using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace TestubgCodeWpfApp
{
    class Timer
    {
        public float Second { get => second; set => second = value; }
        public float Minute { get => minute; set => minute = value; }
        public float Hour { get => hour; set => hour = value; }

        #region Private 
        private float hour;
        private float minute;
        private float second;
        private float minusValue = 0;
        DispatcherTimer dispatcherTimer;

        #endregion

        public Timer(float h, float m, float s)
        {
            Hour = h;
            Minute = m;
            Second = s;
        }

        private void CountDown(object sender, EventArgs e)
        {
            //Debug.WriteLine(Second - minusValue);

            CommandManager.InvalidateRequerySuggested();
            
            minusValue++;

            if (minusValue > Second)
            {
                dispatcherTimer.Stop();
                Debug.WriteLine("Done");
                minusValue = 00;
            }
        }

        public float ReturnHours()
        {
            return Hour;
        }

        public float ReturnMinutes()
        {
            return Minute;
        }

        public float ReturnSeconds()
        {
            Second--;

            return Second;
        }


        public void ShutDown(string timeSec)
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t "+ timeSec);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

    }
}
