using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace RenderImageLibrary
{
    public partial class WideTileControl : UserControl
    {
        public WideTileControl(string tileText)
        {
            InitializeComponent();

            Text = tileText;
        }

        public string Text { get; set; }

        public event EventHandler<SaveJpegCompleteEventArgs> SaveJpegComplete;

        public void BeginSaveJpeg()
        {
            // Load the background image directly - important thing is to delay 
            // rendering the image to the WriteableBitmap until the image has
            // finished loading
            BitmapImage backgroundImage = new BitmapImage(new Uri("DefaultWideTile.jpg", UriKind.Relative));
            backgroundImage.CreateOptions = BitmapCreateOptions.None;

            backgroundImage.ImageOpened += (s, args) =>
            {
                try
                {
                    // Set the loaded image
                    BackgroundImage.Source = backgroundImage;

                    // Set the text
                    TextOverlayTextBox.Text = this.Text;

                    // Explicitly size the control - for use in a background agent
                    this.UpdateLayout();
                    this.Measure(new Size(691, 336));
                    this.UpdateLayout();
                    this.Arrange(new Rect(0, 0, 691, 336));

                    var wb = new WriteableBitmap(691, 336);
                    wb.Render(LayoutRoot, null);
                    wb.Invalidate();

                    // Create a filename for JPEG file in isolated storage
                    // Tile images *must* be in shared/shellcontent.
                    String fileName = "Tile_" + Guid.NewGuid().ToString() + ".jpg";

                    var myStore = IsolatedStorageFile.GetUserStoreForApplication();
                    if (!myStore.DirectoryExists("shared/shellcontent"))
                    {
                        myStore.CreateDirectory("shared/shellcontent");
                    }

                    using (IsolatedStorageFileStream myFileStream = myStore.CreateFile("shared/shellcontent/" + fileName))
                    {
                        // Encode WriteableBitmap object to a JPEG stream.
                        wb.SaveJpeg(myFileStream, wb.PixelWidth, wb.PixelHeight, 0, 75);
                        myFileStream.Close();
                    }

                    // Delete images from earlier execution
                    var filesTodelete = from f in myStore.GetFileNames("shared/shellcontent/Tile_*").AsQueryable()
                                        where !f.EndsWith(fileName)
                                        select f;
                    foreach (var file in filesTodelete)
                    {
                        myStore.DeleteFile("shared/shellcontent/" + file);
                    }

                    // Fire completion event
                    if (SaveJpegComplete != null)
                    {
                        SaveJpegComplete(this, new SaveJpegCompleteEventArgs(true, "shared/shellcontent/" + fileName));
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
}