using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace NeverQ_01
{
    public partial class CheckInPage : PhoneApplicationPage
    {
        public CheckInPage()
        {
            InitializeComponent();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkinbox.Text == "1234")
            {
                MessageBox.Show("Check In Success!");
            }
            else
            {
                MessageBox.Show("Fail!");
            }
        }


    }
}