﻿#pragma checksum "D:\Dropbox\NeverQ(Windows Phone App)\NeverQ_01\NeverQ_01\StatusPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "11FC202E52FB460E6186E0A6D95039DF"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.34011
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace NeverQ_01 {
    
    
    public partial class StatusPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock CurrentNo;
        
        internal System.Windows.Controls.TextBlock StoreName;
        
        internal System.Windows.Controls.TextBlock ExpectTime;
        
        internal System.Windows.Controls.TextBlock NoReadyQ;
        
        internal System.Windows.Controls.TextBlock Now_at;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/NeverQ_01;component/StatusPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.CurrentNo = ((System.Windows.Controls.TextBlock)(this.FindName("CurrentNo")));
            this.StoreName = ((System.Windows.Controls.TextBlock)(this.FindName("StoreName")));
            this.ExpectTime = ((System.Windows.Controls.TextBlock)(this.FindName("ExpectTime")));
            this.NoReadyQ = ((System.Windows.Controls.TextBlock)(this.FindName("NoReadyQ")));
            this.Now_at = ((System.Windows.Controls.TextBlock)(this.FindName("Now_at")));
        }
    }
}

