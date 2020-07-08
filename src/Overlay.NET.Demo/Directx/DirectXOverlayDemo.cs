using Process.NET;
using Process.NET.Memory;
using System;
using System.Globalization;
using System.Linq;

namespace Overlay.NET.Demo.Directx
{
    public class DirectXOverlayDemo
    {
        private OverlayPlugin _directXoverlayPluginExample;
        private ProcessSharp _processSharp;

        public void StartDemo()
        {
            Console.Title = @"Время заказов для Дальнобойщика";
            //var processName = "notepad"; //имя процесса
            var processName = "GTA5"; //имя процесса

            var process = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Игра не найдена.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ожидание запуска игры...");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                StartDemo();
            }

            _directXoverlayPluginExample = new DirectxOverlayPluginExample();
            _processSharp = new ProcessSharp(process, MemoryType.Remote);
            var result = "30";

            var fpsValid = int.TryParse(Convert.ToString(result, CultureInfo.InvariantCulture), NumberStyles.Any,
                NumberFormatInfo.InvariantInfo, out int fps);

            var d3DOverlay = (DirectxOverlayPluginExample)_directXoverlayPluginExample;
            d3DOverlay.Settings.Current.UpdateRate = 1000 / fps;
            _directXoverlayPluginExample.Initialize(_processSharp.WindowFactory.MainWindow);
            _directXoverlayPluginExample.Enable();

            // Log some info about the overlay.

            var info = d3DOverlay.Settings.Current;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Автор: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ info.Author}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Оверлей: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ info.Identifier}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Версия: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{ info.Version}");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Чтобы выключить оверлей - закройте консоль.");

            while (true)
            {
                _directXoverlayPluginExample.Update();
            }

        }
    }
}