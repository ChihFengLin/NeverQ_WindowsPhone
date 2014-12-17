using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RenderImageLibrary;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using Windows.Phone.System.UserProfile;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;

namespace NeverQ_01
{
    public partial class GetNumberPage4 : PhoneApplicationPage
    {
        MainPage trigger = new MainPage();
        DispatcherTimer tmr = new DispatcherTimer();
        public GetNumberPage4()
        {
            InitializeComponent();

            CurrentNo.Text = "44";
            ExpectTime.Text = "33";

            tmr.Interval = TimeSpan.FromSeconds(20.0f);
            tmr.Tick += onTimerTick;
            tmr.Start();
        }

        void onTimerTick(object sender, EventArgs e)
        {
            CurrentNo.Text = "46";
            ExpectTime.Text = "36";
        }

        void NumberCard_ManipulationDelta(object snder, ManipulationDeltaEventArgs e)
        {
            //dragTraslation.X += e.DeltaManipulation.Translation.X;
            //dragTraslation.Y += e.DeltaManipulation.Translation.Y;
            if (this.translation.Y >= 0)
            {
                this.translation.Y += e.DeltaManipulation.Translation.Y;
            }
            else
            {
                this.translation.Y = 0;
            }
        }

        private void NumberCard_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            trigger.InvokePeriodicAgent();    //這邊可以傳參數進去，用以判斷是取哪間餐廳的號碼，進而接收不同的後台SERVER
            MessageBox.Show("您的號碼為: ");
            this.translation.Y = 0;
        }

        private void NumberCard_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {

        }

        private void Menu_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            this.translation1.Y += e.DeltaManipulation.Translation.Y;
        }

        private void Menu_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GetNumberPage5_Menu.xaml", UriKind.Relative));
            this.translation1.Y = 0;
        }
    }
}