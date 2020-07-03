using Overlay.NET.Common;
using Overlay.NET.Demo.Internals;
using Overlay.NET.Directx;
using Process.NET.Windows;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Overlay.NET.Demo.Directx
{
    [RegisterPlugin("Специально для Grand RP (GTA V)", "MjKey", "Время заказов для дальнобойщиков", "1.3 BETA",
        "Тестовая версия.")]
    public class DirectxOverlayPluginExample : DirectXOverlayPlugin
    {
        private readonly TickEngine _tickEngine = new TickEngine();
        public readonly ISettings<DemoOverlaySettings> Settings = new SerializableSettings<DemoOverlaySettings>();
        private int _displayFps;
        private int _font;
        private int fB;
        private int _hugeFont;
        private int _i;
        private int _interiorBrush;
        private int _redBrush;
        private int _BlackBrush;
        private int _redOpacityBrush;
        private float _rotation;
        private Stopwatch _watch;
        private int r;
        private int s;
        private string ValS;
        static double WI = System.Windows.SystemParameters.PrimaryScreenWidth;
        private int WiGo = (int)(WI * 42 / 100);




        public override void Initialize(IWindow targetWindow)
        {
            // Set target window by calling the base method
            base.Initialize(targetWindow);

            // For demo, show how to use settings
            var current = Settings.Current;
            var type = GetType();

            if (current.UpdateRate == 0)
            {
                current.UpdateRate = 1000 / 60;
            }

            current.Author = GetAuthor(type);
            current.Description = GetDescription(type);
            current.Identifier = GetIdentifier(type);
            current.Name = GetName(type);
            current.Version = GetVersion(type);

            // File is made from above info
            Settings.Save();
            Settings.Load();
        Minuta:
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Введите время следущего заказа (минуты)");
            Console.WriteLine("Например: 26");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Минуты: ");
            r = int.Parse(Console.ReadLine()); //ввод
            //r = 2;
            if (r >= 0 && r <= 60)
            {
                Zvuk:
                Console.Clear();
                Console.WriteLine("Включить звуковое оповещение?");
                Console.WriteLine("1 - Да | 0 - Нет (по умолчанию)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Значение: ");
                s = int.Parse(Console.ReadLine()); //ввод
                if (s == 1 || s == 0)
                {
                    Console.Clear();
                    void WriteLineCentered(string text)
                    {
                        int width = Console.WindowWidth;
                        if (text.Length < width)
                        {
                            text = text.PadLeft((width - text.Length) / 2 + text.Length, ' ');
                        }
                        Console.WriteLine(text);
                    }
                    if (s == 1)
                    {
                        ValS = "включен";
                    }
                    else
                    {
                        ValS = "выключен";
                    }

                    WriteLineCentered("ОВЕРЛЕЙ ЗАПУЩЕН | Начальная временная отметка: " + r + " | Звук " + ValS);
                    if (s == 1)
                    {
                        System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer(Properties.Resources.coin04);
                        SoundPlayer.Play();
                    }

                    OverlayWindow = new DirectXOverlayWindow(targetWindow.Handle, false);
                    _watch = Stopwatch.StartNew();

                    _redBrush = OverlayWindow.Graphics.CreateBrush(0x7FFF0000);
                    _redOpacityBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(255, 225, 0, 0));
                    _BlackBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(245, 0, 0, 0));
                    _interiorBrush = OverlayWindow.Graphics.CreateBrush(0x7FFFFF00);

                    _font = OverlayWindow.Graphics.CreateFont("Arial", 22, true);
                    fB = OverlayWindow.Graphics.CreateFont("Arial", 24, true);
                    _hugeFont = OverlayWindow.Graphics.CreateFont("Arial", 50, true);

                    _rotation = 0.0f;
                    _displayFps = 0;
                    _i = 0;
                    // Set up update interval and register events for the tick engine.

                    _tickEngine.PreTick += OnPreTick;
                    _tickEngine.Tick += OnTick;
                }
                else
                {
                    goto Zvuk;
                }
            }
            else
            {
                goto Minuta;
            }
           
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!OverlayWindow.IsVisible)
            {
                OverlayWindow.Update();
            }

            OverlayWindow.Update();
            InternalRender();

        }

        private void OnPreTick(object sender, EventArgs e)
        {
            var targetWindowIsActivated = TargetWindow.IsActivated;
            if (!targetWindowIsActivated && OverlayWindow.IsVisible)
            {
               // _watch.Stop();
               // ClearScreen();
                OverlayWindow.Hide();
            }
            else if (targetWindowIsActivated && !OverlayWindow.IsVisible)
            {
                OverlayWindow.Show();
                
            }
        }

        // ReSharper disable once RedundantOverriddenMember
        public override void Enable()
        {
            _tickEngine.Interval = Settings.Current.UpdateRate.Milliseconds();
            _tickEngine.IsTicking = true;
            base.Enable();
        }


        // ReSharper disable once RedundantOverriddenMember
        public override void Disable()
        {
            _tickEngine.IsTicking = false;
            base.Disable();
        }

        public override void Update() => _tickEngine.Pulse();

        protected void InternalRender()
        {
            /*if (!_watch.IsRunning)
            {
                _watch.Start();
            }*/
            _watch.Start();
            OverlayWindow.Graphics.BeginScene();
            OverlayWindow.Graphics.ClearScene();

            //first row
            int m = DateTime.Now.Minute;
            int h = DateTime.Now.Hour;
            int hr = h;
            if (r == m)
            {
                r = m + 7;
                if (s == 1)
                {
                    System.Media.SoundPlayer SoundPlayerX = new System.Media.SoundPlayer(Properties.Resources.x);
                    SoundPlayerX.Play();
                }
            }

            if (r >= 60)
            {
                r = r - 60;
                hr++;
            }

            string ts = "";

            if (hr < 10)
            {
                ts += "0" + hr;
            }
            else
            {
                ts += hr;
            }

            ts += ":";

            if (r < 10)
            {
                ts += "0" + r;
            }
            else
            {
                ts += r;
            }

            //// 

            if (m >= 60)
            {
                m = m - 60;
                h++;
            }


            string time = "";

            if (h < 10)
            {
                time += "0" + h;
            }
            else
            {
                time += h;
            }

            time += ":";

            if (m < 10)
            {
                time += "0" + m;
            }
            else
            {
                time += m;
            }


            //OverlayWindow.Graphics.DrawText(time, _font, _redBrush, 400, 40);

            OverlayWindow.Graphics.DrawText("Следующий заказ: " + ts, fB, _BlackBrush, WiGo+2, 30);

            OverlayWindow.Graphics.DrawText("Следующий заказ: " + ts, _font, _redOpacityBrush, WiGo, 30);

            _rotation += 0.03f; //related to speed

            if (_rotation > 50.0f) //size of the swastika
            {
                _rotation = -50.0f;
            }

            if (_watch.ElapsedMilliseconds > 1000)
            {
                _i = _displayFps;
                _displayFps = 0;
                _watch.Restart();
            }

            else
            {
                _displayFps++;
            }

            //OverlayWindow.Graphics.DrawText("FPS: " + _i, _hugeFont, _redBrush, 840, 600, false);

            OverlayWindow.Graphics.EndScene();

        }


public override void Dispose()
        {
            OverlayWindow.Dispose();
            base.Dispose();
        }

        private void ClearScreen()
        {
            OverlayWindow.Graphics.BeginScene();
            OverlayWindow.Graphics.ClearScene();
            OverlayWindow.Graphics.EndScene();
        }
    }
}