using System.ServiceProcess;
using System.Timers;

namespace Thinkpad_Auto_Backlight_Service
{
    public partial class Service1 : ServiceBase
    {
        private readonly Keyboard_Core.KeyboardControl core = new Keyboard_Core.KeyboardControl();
        private readonly Timer timer = new Timer();
        private enum Status { off, mid, high };

        private Status status { get; set; }

        public Service1()
        {
            InitializeComponent();
            CanHandlePowerEvent = true;
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            switch (powerStatus)
            {
                case PowerBroadcastStatus.ResumeAutomatic:
                    SetKeyboardBacklight(status);
                    timer.Start();
                    break;
                case PowerBroadcastStatus.Suspend:
                    timer.Stop();                    
                    break;
                default:
                    break;
            }
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 3000; //poll every 3 seconds
            timer.Enabled = true;

            GetKeyboardBacklight();
            SetKeyboardBacklight(Status.high);
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            GetKeyboardBacklight();
        }

        private void GetKeyboardBacklight()
        {
            core.GetKeyboardBackLightStatus(out int keyboardStatus);
            status = (Status)keyboardStatus;            
        }

        private void SetKeyboardBacklight(Status _status)
        {
            //Set keyboard back light to brightest
            core.SetKeyboardBackLightStatus((int)_status);
            status = _status;
        }

        protected override void OnStop()
        {
        }
    }
}
