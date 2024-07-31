using System;
using System.Reactive;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using JuliusSweetland.OptiKey.Contracts;
using JuliusSweetland.OptiKey.Static;
using System.Runtime.InteropServices;
using System.IO;

namespace OptikeyPlugins
{

    public class BeamInput : IPointService, IDisposable
    {
        #region Fields
        private event EventHandler<Timestamped<Point>> pointEvent;

        private BackgroundWorker pollWorker;

        private IntPtr trackerClient = IntPtr.Zero;

        private readonly FileLogger _logger;

        #endregion

        #region Ctor

        public BeamInput()
        {
            // Set up text file for logging
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string logFilePath = Path.Combine(appDataPath, "Optikey", "beam_log.txt");
            _logger = new FileLogger(logFilePath);

            // Connect to client
            // We don't test connection because it often lies
            _logger.Log("Constructing tracker client");
            try
            {
                trackerClient = BeamWrapper.create_tracker_instance("127.0.0.1", 12010);
                _logger.Log("Done");
            }
            catch (Exception e)
            {
                _logger.Log(e.ToString());
                PublishError(this, e);
            }
            
            pollWorker = new BackgroundWorker();
            pollWorker.DoWork += pollMouse;
            pollWorker.WorkerSupportsCancellation = true;
        }

        public void Dispose()
        {
            pollWorker.CancelAsync();
            pollWorker.Dispose();

            lock (this)
            {
                if (trackerClient != IntPtr.Zero)
                {
                    BeamWrapper.release_tracker_instance(trackerClient);
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        #endregion

        #region Events

        public event EventHandler<Exception> Error;

        public event EventHandler<Timestamped<Point>> Point
        {
            add
            {
                if (pointEvent == null)
                {
                    // Start polling the beam tracker
                    pollWorker.RunWorkerAsync();
                }

                pointEvent += value;
            }
            remove
            {
                pointEvent -= value;

                if (pointEvent == null)
                {
                    pollWorker.CancelAsync();
                }
            }
        }

        #endregion

        #region Private methods        

        private void pollMouse(object sender, DoWorkEventArgs e)
        {
            while (!pollWorker.CancellationPending)
            {
                _logger.Log("poll");
                lock (this)
                {
                    if (trackerClient != IntPtr.Zero)
                    { // &&
                      //    BeamWrapper.connected(trackerClient) // currently connected always says false...
                      //    )

                        _logger.Log("Querying tracker");

                        var timeStamp = Time.HighResolutionUtcNow.ToUniversalTime();
                        ScreenGazeInfo info = BeamWrapper.get_screen_gaze_info(trackerClient);

                        _logger.Log($"Point event: {info.x}, {info.y}");

                        // Emit a point event
                        pointEvent(this, new Timestamped<Point>(
                            new Point(info.x, info.y),
                            timeStamp));
                    }

                    // Sleep thread to avoid hot loop
                    int delay = 30; // ms
                    Thread.Sleep(delay);
                }
            }
        }
        #endregion

        #region Publish Error

        private void PublishError(object sender, Exception ex)
        {
            if (Error != null)
            {
                Error(sender, ex);
            }
        }

        #endregion
    }
}
