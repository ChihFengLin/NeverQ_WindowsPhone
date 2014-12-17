#define DEBUG_AGENT
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Scheduler;
using Windows.Phone.System.UserProfile;
using System.Diagnostics;
using NeverQ_01.Resources;
using ScheduledTaskAgent1;
using System.Windows.Threading;

namespace NeverQ_01
{
    public partial class MainPage : PhoneApplicationPage
    {
        //bool BackgroundAgentIsInvoke = false;
        
        DispatcherTimer tmr = new DispatcherTimer();
        
        // 建構函式
        public MainPage()
        {
            
            InitializeComponent();

            NoReadyQ.Text = "No Ready Queue";

            StoreName.Text = " ";
            ExpectTime.Text = " ";
            CurrentNo.Text = " ";

            // 將清單方塊控制項的資料內容設為範例資料
            DataContext = App.ViewModel;

            tmr.Interval = TimeSpan.FromSeconds(20.0f);
            tmr.Tick += onTimerTick;
            tmr.Start();

        }

        void onTimerTick(object sender, EventArgs e)
        {
            StoreName.Text = "鼎泰豐";
            CurrentNo.Text = "46";
            ExpectTime.Text = "36";
            NoReadyQ.Text = " ";
        }

        // 進入頁面的初始化動作
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

        }

        private void StartPeriodicAgent()
        {
            PeriodicTask periodicTask = new PeriodicTask("ScheduledTaskAgent1");

            periodicTask.Description = "test";

            // If the agent is already registered with the system,
            if (ScheduledActionService.Find(periodicTask.Name) != null)
            {
                ScheduledActionService.Remove("ScheduledTaskAgent1");
            }

            //only can be called when application is running in foreground
            ScheduledActionService.Add(periodicTask);

            ScheduledActionService.LaunchForTest(periodicTask.Name, TimeSpan.FromSeconds(5.0f));
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing  
            if (listBox.SelectedIndex == -1)    //防止listbox只能被觸發一次
                return;

            ListBox lb = (ListBox)sender;
            int number = lb.SelectedIndex+1;

            switch (number)
            {
                case 1:
                    
                    NavigationService.Navigate(new Uri("/GetNumberPage1.xaml", UriKind.Relative));
                    break;
                case 2:
                    NavigationService.Navigate(new Uri("/GetNumberPage2.xaml", UriKind.Relative));
                    break;
                case 3:
                    NavigationService.Navigate(new Uri("/GetNumberPage3.xaml", UriKind.Relative));
                    break;
                case 4:
                    NavigationService.Navigate(new Uri("/GetNumberPage4.xaml", UriKind.Relative));
                    break;
                case 5:
                    NavigationService.Navigate(new Uri("/GetNumberPage5.xaml", UriKind.Relative));
                    break;
            }

            // Reset selected index to -1 (no selection)  
            listBox.SelectedIndex = -1;       
        }



        public void InvokePeriodicAgent()    //string status
        {
            //BackgroundAgentIsInvoke = true;
            StartPeriodicAgent();

            Dispatcher.BeginInvoke(() =>
            {
                // Render the new tile image
                RenderImageLibrary.WideTileControl wtc = new RenderImageLibrary.WideTileControl(
                    "In 8 Minutes");               //綁定現在號碼

                wtc.SaveJpegComplete += async (s, args) =>
                {
                    try
                    {
                        if (args.Success)
                        {
                            // Set the tile image URI - "isostore:/" is important! Note that the control already
                            // puts the image into /Shared/ShellContent which is where tile images in the local folder must be
                            Uri tileImageUri = new Uri("isostore:/" + args.ImageFileName, UriKind.RelativeOrAbsolute);
                            Debug.WriteLine(tileImageUri.ToString());

                            // Set the tile image
                            FlipTileData ftd = new FlipTileData();
                            ftd.WideBackgroundImage = tileImageUri;
                            ftd.Title = " ";

                            ShellTile.ActiveTiles.First().Update(ftd);
                        }
                        else
                        {
                            Debug.WriteLine(args.Exception.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                };

                wtc.BeginSaveJpeg();

            }
    );
        }


        private void Canvas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StatusPage.xaml", UriKind.Relative));
        }

    }
}