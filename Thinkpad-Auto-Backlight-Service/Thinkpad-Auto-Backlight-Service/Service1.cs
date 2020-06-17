using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Thinkpad_Auto_Backlight_Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Keyboard_Core.KeyboardControl core = new Keyboard_Core.KeyboardControl();
            //Set keyboard back light to brightest
            core.SetKeyboardBackLightStatus(2);
            //Stop the service
            Stop();
        }

        protected override void OnStop()
        {
        }
    }
}
