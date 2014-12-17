using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace NeverQ_01
{
    public partial class WelcomPage1 : PhoneApplicationPage
    {
        DispatcherTimer tmr = new DispatcherTimer();
        public WelcomPage1()
        {
            InitializeComponent();
            tmr.Interval = TimeSpan.FromSeconds(3.5f);
            tmr.Tick += onTimerTick;
            tmr.Start();
        }

        void onTimerTick(object sender, EventArgs e)
        {
            tmr.Stop();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }


        private void Rectangle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}