using Overlay.NET.Common;
using Overlay.NET.Demo.Internals;
using Overlay.NET.Directx;
using Process.NET.Windows;
using System;
using System.Diagnostics;
using Color = System.Drawing.Color;

namespace Overlay.NET.Demo.Directx
{
    [RegisterPlugin("Специально для Grand RP (GTA V)", "MjKey", "Время заказов для дальнобойщиков", "1.4",
        "BETA - версия.")]
    public class DirectxOverlayPluginExample : DirectXOverlayPlugin
    {
        private readonly TickEngine _tickEngine = new TickEngine();
        public readonly ISettings<DemoOverlaySettings> Settings = new SerializableSettings<DemoOverlaySettings>();
        private int _font;
        private int _font2;
        private int _BlackBrush;
        private int _redOpacityBrush;
        private Stopwatch _watch;
        private string r;
        private int r1;
        private int r2;
        private int r3;
        private int r4;
        private int r5;
        private int r6;
        private string r1t;
        private string r2t;
        private string r3t;
        private string r4t;
        private string r5t;
        private string r6t;
        private string r7t;
        private int r7;
        private string s;
        private string ValS;
        static readonly double WI = System.Windows.SystemParameters.PrimaryScreenWidth;
        private readonly int WiGo = (int)(WI * 42 / 100);



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

        Minuta:
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Введите время следущего заказа (минуты)");
            Console.WriteLine("Например: 3");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Минуты: ");

            r = Console.ReadLine(); //ввод
            //r = 2;
            if (r.Length == 0)
            {
                r = Convert.ToString(DateTime.Now.Minute);
            }
            if (Convert.ToInt32(r) >= 0 && Convert.ToInt32(r) <= 60)
            {
            Zvuk:
                Console.Clear();
                Console.WriteLine("Включить звуковое оповещение?");
                Console.WriteLine("1 - Да | 0 - Нет (по умолчанию)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Значение: ");

                s = Console.ReadLine(); //ввод
                if (s.Length == 0)
                {
                    s = Convert.ToString(0);
                }
                if (Convert.ToInt32(s) == 1 || Convert.ToInt32(s) == 0)
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
                    if (Convert.ToInt32(s) == 1)
                    {
                        ValS = "включен";
                    }
                    else
                    {
                        ValS = "выключен";
                    }

                    WriteLineCentered("ОВЕРЛЕЙ ЗАПУЩЕН | Начальная временная отметка: " + r + " | Звук " + ValS);
                    if (Convert.ToInt32(s) == 1)
                    {
                        System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer(Properties.Resources.coin04);
                        SoundPlayer.Play();
                    }

                    OverlayWindow = new DirectXOverlayWindow(targetWindow.Handle, false);
                    _watch = Stopwatch.StartNew();

                    _redOpacityBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(255, 225, 0, 0));
                    _BlackBrush = OverlayWindow.Graphics.CreateBrush(Color.FromArgb(245, 0, 0, 0));

                    _font = OverlayWindow.Graphics.CreateFont("Arial", 22, true);
                    _font2 = OverlayWindow.Graphics.CreateFont("Arial", 18, true);

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
            if (Convert.ToInt32(r) == m)
            {
                r = Convert.ToString(m + 7);
                if (Convert.ToInt32(r) == 1)
                {
                    System.Media.SoundPlayer SoundPlayerX = new System.Media.SoundPlayer(Properties.Resources.x);
                    SoundPlayerX.Play();
                }
            }

            r1 = Convert.ToInt32(r) + 7;
            if (r1 < 60)
            {
                r1t = " | " + r1;
            }
            else
            {
                r1t = "";
            }
            r2 = r1 + 7;
            if (r2 < 60)
            {
                r2t = " | " + r2;
            }
            else
            {
                r2t = "";
            }
            r3 = r2 + 7;
            if (r3 < 60)
            {
                r3t = " | " + r3;
            }
            else
            {
                r3t = "";
            }
            r4 = r3 + 7;
            if (r4 < 60)
            {
                r4t = " | " + r4;
            }
            else
            {
                r4t = "";
            }
            r5 = r4 + 7;
            if (r5 < 60)
            {
                r5t = " | " + r5;
            }
            else
            {
                r5t = "";
            }
            r6 = r5 + 7;
            if (r6 < 60)
            {
                r6t = " | " + r6;
            }
            else
            {
                r6t = "";
            }
            r7 = r6 + 7;
            if (r7 < 60)
            {
                r7t = " | " + r7;
            }
            else
            {
                r7t = "";
            }


            if (Convert.ToInt32(r) >= 60)
            {
                r = Convert.ToString(Convert.ToInt32(r) - 60);
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

            if (Convert.ToInt32(r) < 10)
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


            //OverlayWindow.Graphics.DrawText(time, _font, _redBrush, 400, 40);

            OverlayWindow.Graphics.DrawText("Следующий заказ: " + ts, _font, _BlackBrush, WiGo + 1, 30);

            OverlayWindow.Graphics.DrawText("Следующий заказ: " + ts, _font, _redOpacityBrush, WiGo, 30);

            OverlayWindow.Graphics.DrawText(r + r1t + r2t + r3t + r4t + r5t + r6t + r7t, _font2, _BlackBrush, WiGo + 1, 60);

            OverlayWindow.Graphics.DrawText(r + r1t + r2t + r3t + r4t + r5t + r6t + r7t, _font2, _redOpacityBrush, WiGo, 60);

            //OverlayWindow.Graphics.DrawText("FPS: " + _i, _hugeFont, _redBrush, 840, 600, false);

            OverlayWindow.Graphics.EndScene();

        }


        public override void Dispose()
        {
            OverlayWindow.Dispose();
            base.Dispose();
        }
    }
}