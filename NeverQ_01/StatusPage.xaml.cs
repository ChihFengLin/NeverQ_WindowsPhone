using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;

namespace NeverQ_01
{
    public partial class StatusPage : PhoneApplicationPage
    {
        DispatcherTimer tmr = new DispatcherTimer();
        public StatusPage()
        {
            InitializeComponent();

            NoReadyQ.Text= "No Ready Queue";
            
            Now_at.Text = " ";         //先post一個當下的值來做initialize
            StoreName.Text = " ";
            CurrentNo.Text = " ";
            ExpectTime.Text = " ";
            
            tmr.Interval = TimeSpan.FromSeconds(20.0f);
            tmr.Tick += onTimerTick;
            tmr.Start();

        }

        void onTimerTick(object sender, EventArgs e)
        {
            Now_at.Text = "35";
            StoreName.Text = "鼎泰豐";
            CurrentNo.Text = "48";
            ExpectTime.Text = "20";
            NoReadyQ.Text = " ";
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CheckInPage.xaml", UriKind.Relative));
        }

        private void ImageButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ImageButton_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}