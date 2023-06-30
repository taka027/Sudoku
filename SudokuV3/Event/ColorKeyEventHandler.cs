using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace SudokuV3
{
    internal class ColorKeyEventHandler : KeyEventHandlerBase
    {
        public void KeyDown(object sender, KeyEventArgs e)
        {
            int num = -1;
            if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
            { 
                if (e.Key == Key.D1)
                {
                    num = 1;
                }
                if (e.Key == Key.D2)
                {
                    num = 2;
                }
                if (e.Key == Key.D3)
                {
                    num = 3;
                }
                if (e.Key == Key.D4)
                {
                    num = 4;
                }
                if (e.Key == Key.D5)
                {
                    num = 5;
                }
                if (e.Key == Key.D6)
                {
                    num = 6;
                }
                if (e.Key == Key.D7)
                {
                    num = 7;
                }
                if (e.Key == Key.D8)
                {
                    num = 8;
                }
                if (e.Key == Key.D9)
                {
                    num = 9;
                }
            }
            if (e.Key == Key.D)
            {
                num = 0;
            }

            // 数字色付け
            if (num >= 0)
            {
                // 一旦色クリア
                this.ColorNumber(0);
                this.ColorNumber(num);
            }
        }

        private void ColorNumber(int num)
        {
            // 一旦色クリア
            if (num == 0)
            {
                // 全セルの色付け
                for (var counterX = 0; counterX < 9; counterX++)
                {
                    for (var counterY = 0; counterY < 9; counterY++)
                    {
                        Type type = this.MainWindow.GetType();
                        var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + counterY));
                        if (field != null)
                        {
                            // 色付け
                            EventUtility.Paint(this.MainWindow, counterX, counterY, Brushes.White);
                        }
                    }
                }

                return;
            }

            // 各列の色付け
            for (var counterX = 0; counterX < 9; counterX++)
            {
                for (var counterY = 0; counterY < 9; counterY++)
                {
                    Type type = this.MainWindow.GetType();
                    var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + counterY));
                    if (field != null)
                    {
                        var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                        if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length > 1)
                        {
                            // 数字メモとして入っているとき
                            for (var i = 0; i < target.Text.Length; i++)
                            {
                                if (num == (int)Char.GetNumericValue(target.Text.ElementAt(i)))
                                {
                                    // 色付け
                                    EventUtility.Paint(this.MainWindow, counterX, counterY, Brushes.Yellow, Brushes.DarkOrange, Brushes.OrangeRed);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
