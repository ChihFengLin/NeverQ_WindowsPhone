using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using RenderImageLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows; 

namespace ScheduledTaskAgent1
{
    
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent 建構函式，會初始化 UnhandledException 處理常式
        /// </remarks>
        static ScheduledAgent()
        {
            // 訂閱 Managed 例外狀況處理常式
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// 發生未處理的例外狀況時要執行的程式碼
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // 發生未處理的例外狀況; 切換到偵錯工具
                Debugger.Break();
            }
        }

        /// <summary>
        /// 執行排程工作的代理程式
        /// </summary>
        /// <param name="task">
        /// 叫用的工作
        /// </param>
        /// <remarks>
        /// 這個方法的呼叫時機為叫用週期性或耗用大量資料的工作時
        /// </remarks>

            
    protected override void OnInvoke(ScheduledTask task)
             {
        
            // Render a new image for the lock screen
            System.Threading.ManualResetEvent mre = new System.Threading.ManualResetEvent(false);

/*            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (Windows.Phone.System.UserProfile.LockScreenManager.IsProvidedByCurrentApplication)
                {
                    LockScreenImageControl cont = new LockScreenImageControl();
                    cont.SaveJpegComplete += (s, args) =>
                        {
                            if (args.Success)
                            {
                                // Set the lock screen image URI - App URI syntax is required! "ms-appdata:///Local/..." 
                                Uri lockScreenImageUri = new Uri("ms-appdata:///Local/" + args.ImageFileName, UriKind.Absolute);
                                Debug.WriteLine(lockScreenImageUri.ToString());
                                Windows.Phone.System.UserProfile.LockScreen.SetImageUri(lockScreenImageUri);
                            }
                            mre.Set(); 
                        };
                    cont.BeginSaveJpeg();
                }
            });

            // Wait for Lock Screen image to complete
            mre.WaitOne();
            // Then reset for the Tile Image operation
            mre.Reset();
*/
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // Render the new tile image
                RenderImageLibrary.WideTileControl wtc = new RenderImageLibrary.WideTileControl(
                    "In 7 Minutes");     //這邊可以binding字串進來

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

                            // Check nothing went wrong and the file exists... will throw exception if file does not exist
                            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
                                new Uri("ms-appdata:///local/" + args.ImageFileName, UriKind.Absolute));

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
                    finally
                    {
                        mre.Set();
                    }
                };

                wtc.BeginSaveJpeg();
            }
    );
            // Wait for tile operation to complete
            mre.WaitOne();

            NotifyComplete();
        }
    }
}