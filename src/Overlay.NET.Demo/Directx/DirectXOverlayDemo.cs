using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Overlay.NET.Common;
using Process.NET;
using Process.NET.Memory;

namespace Overlay.NET.Demo.Directx {
    public class DirectXOverlayDemo {
        private OverlayPlugin _directXoverlayPluginExample;
        private ProcessSharp _processSharp;

        public void StartDemo() {
            var processName = "GTA5"; //имя процесса

            var process = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null) {
                Log.Warn($"Игра не найдена.");
                Log.Warn("Запустите сначало игру, а потом оверлей Дальнобойщика.");
                Console.ReadKey();
                return;
            }

            _directXoverlayPluginExample = new DirectxOverlayPluginExample();
            _processSharp = new ProcessSharp(process, MemoryType.Remote);
            var result = "30";

            var fpsValid = int.TryParse(Convert.ToString(result, CultureInfo.InvariantCulture), NumberStyles.Any,
                NumberFormatInfo.InvariantInfo, out int fps);

            var d3DOverlay = (DirectxOverlayPluginExample) _directXoverlayPluginExample;
            d3DOverlay.Settings.Current.UpdateRate = 1000 / fps;
            _directXoverlayPluginExample.Initialize(_processSharp.WindowFactory.MainWindow);
            _directXoverlayPluginExample.Enable();

            // Log some info about the overlay.
            Log.Debug("Запус модуля..");

            var info = d3DOverlay.Settings.Current;

            Log.Info($"Автор: {info.Author}");
            Log.Info($"Оверлей: {info.Identifier}");
            Log.Info($"Версия: {info.Version}");

            Log.Info("Закройте консоль чтобы выключить оверлей.");

            while (true) {
                _directXoverlayPluginExample.Update();
            }
        }
    }
}