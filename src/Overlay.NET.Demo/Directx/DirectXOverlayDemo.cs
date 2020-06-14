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
            var processName = "GTA5"; //��� ��������

            var process = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null) {
                Log.Warn($"���� �� �������.");
                Log.Warn("��������� ������� ����, � ����� ������� �������������.");
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
            Log.Debug("����� ������..");

            var info = d3DOverlay.Settings.Current;

            Log.Info($"�����: {info.Author}");
            Log.Info($"�������: {info.Identifier}");
            Log.Info($"������: {info.Version}");

            Log.Info("�������� ������� ����� ��������� �������.");

            while (true) {
                _directXoverlayPluginExample.Update();
            }
        }
    }
}