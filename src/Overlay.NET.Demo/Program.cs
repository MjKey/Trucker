using System;
using Overlay.NET.Common;
using Overlay.NET.Demo.Directx;
using Overlay.NET.Demo.Internals;
using Process.NET;

namespace Overlay.NET.Demo {
    /// <summary>
    /// </summary>
    public static class Program {
        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main() {
            Log.Register("Console", new ConsoleLog());
            var directXDemo = new DirectXOverlayDemo();
            directXDemo.StartDemo();
            Log.WriteLine("Оверлей запущен..");
        }
    }
}