using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Threading;

namespace RenderImageLibrary
{
    public partial class LockScreenImageControl : UserControl
    {
        public LockScreenImageControl()
        {
            InitializeComponent();

            TB1.Text = "Created at " + DateTime.Now.ToShortTimeString();
        }

        public event EventHandler<SaveJpegCompleteEventArgs> SaveJpegComplete;

        public void BeginSaveJpeg()
        {
            // Load the background image directly - important thing is to delay 
            // rendering the image to the WriteableBitmap until the image has
            // finished loading
            BitmapImage backgroundImage = new BitmapImage(new Uri("airelectricityLite.jpg", UriKind.Relative));
            backgroundImage.CreateOptions = BitmapCreateOptions.None;

            backgroundImage.ImageOpened += (s, args) =>
            {
                try
                {
                    // Set the loaded image
                    BackgroundImage.Source = backgroundImage;

                    // Explicitly size the control - for use in a background agent
                    this.UpdateLayout();
                    this.Measure(new Size(480, 800));
                    this.UpdateLayout();
                    this.Arrange(new Rect(0, 0, 480, 800));

                    var wb = new WriteableBitmap(480, 800);
                    wb.Render(LayoutRoot, null);
                    wb.Invalidate();
                    // Create a filename for JPEG file in isolated storage.
                    String fileName = "Lock_" + Guid.NewGuid().ToString() + ".jpg";
                    var myStore = IsolatedStorageFile.GetUserStoreForApplication();

                    using (IsolatedStorageFileStream myFileStream = myStore.CreateFile(fileName))
                    {
                        // Encode WriteableBitmap object to a JPEG stream.
                        wb.SaveJpeg(myFileStream, wb.PixelWidth, wb.PixelHeight, 0, 75);
                        myFileStream.Close();
                    }

                    // Delete images from earlier execution
                    var filesTodelete = from f in myStore.GetFileNames("Lock_*").AsQueryable()
                                        where f != fileName
                                        select f;
                    foreach (var file in filesTodelete)
                    {
                        myStore.DeleteFile(file);
                    }

                    // Fire completion event
                    if (SaveJpegComplete != null)
                    {
                        SaveJpegComplete(this, new SaveJpegCompleteEventArgs(true, fileName));
                    }

                }
                catch (Exception ex)
                {
                    // Log it
                    System.Diagnostics.Debug.WriteLine("Exception in SaveJpeg: " + ex.ToString());

                    if (SaveJpegComplete != null)
                    {
                        var args1 = new SaveJpegCompleteEventArgs(false, "");
                        args1.Exception = ex;
                        SaveJpegComplete(this, args1);
                    }
                }
            };

            return;
        }
    }

    public class SaveJpegCompleteEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string ImageFileName { get; set; }

        public SaveJpegCompleteEventArgs(bool success, string fileName)
        {
            Success = success;
            ImageFileName = fileName;
        }
    }
}
