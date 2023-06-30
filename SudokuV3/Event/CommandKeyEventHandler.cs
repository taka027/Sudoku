using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace SudokuV3
{
    internal class CommandKeyEventHandler : KeyEventHandlerBase
    {
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                // 現在の位置の入りうる数字をメモする
                this.InputAutoNumber();
            }
            if (e.Key == Key.Y)
            {
                // 現在の位置の数字メモの数字を色付けする
                this.ColorNumber();
            }
        }

        private void ColorNumber()
        {
            Type type = this.MainWindow.GetType();
            var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + x + "_" + y));
            if (field != null)
            {
                var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                if (target != null)
                {
                    if (!string.IsNullOrEmpty(target.Text))
                    {
                        // 何か入っているとき
                        // 数字をひとつづつ取り出す
                        for (var i = 0; i < target.Text.Length; i++)
                        {
                            var num = (int)Char.GetNumericValue(target.Text.ElementAt(i));

                            // 各列の色付け
                            for (var counterX = 0; counterX < 9; counterX++)
                            {
                                for (var counterY = 0; counterY < 9; counterY++)
                                {
                                    Type targetType = this.MainWindow.GetType();
                                    var targetField = targetType.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + counterY));
                                    if (targetField != null)
                                    {
                                        var targetCell = (System.Windows.Controls.TextBlock?)targetField.GetValue(this.MainWindow);
                                        if (targetCell != null && !string.IsNullOrEmpty(targetCell.Text) && targetCell.Text.Length > 1)
                                        {
                                            // 数字メモとして入っているとき
                                            for (var j = 0; j < targetCell.Text.Length; j++)
                                            {
                                                if (num == (int)Char.GetNumericValue(targetCell.Text.ElementAt(j)))
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
            }
        }

        private void InputAutoNumber()
        {
            // 最初にクリア
            this.InputNumber(x, y, 0);

            var numbers = new HashSet<int>();
            // 現在の行のすべてのセルから入っている数字を全種類取り出す
            for (var counterX = 0; counterX < 9; counterX++)
            {
                Type type = this.MainWindow.GetType();
                var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + y));
                if (field != null)
                {
                    var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                    if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length == 1)
                    {
                        // ひとつの数字として入っているとき
                        for (var i = 0; i < target.Text.Length; i++)
                        {
                            numbers.Add((int)Char.GetNumericValue(target.Text.ElementAt(i)));
                        }
                    }
                }
            }

            // 現在の列のすべてのセルから入っている数字を全種類取り出す
            for (var counterY = 0; counterY < 9; counterY++)
            {
                Type type = this.MainWindow.GetType();
                var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + x + "_" + counterY));
                if (field != null)
                {
                    var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                    if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length == 1)
                    {
                        // ひとつの数字として入っているとき
                        for (var i = 0; i < target.Text.Length; i++)
                        {
                            numbers.Add((int)Char.GetNumericValue(target.Text.ElementAt(i)));
                        }
                    }
                }
            }

            // 現在のブロックのすべてのセルから入っている数字を全種類取り出す
            for (var counterX = (x / 3) * 3; counterX < (x / 3) * 3 + 3; counterX++)
            {
                for (var counterY = (y / 3) * 3; counterY < (y / 3) * 3 + 3; counterY++)
                {
                    Type type = this.MainWindow.GetType();
                    var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + counterY));
                    if (field != null)
                    {
                        var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                        if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length == 1)
                        {
                            // ひとつの数字として入っているとき
                            for (var i = 0; i < target.Text.Length; i++)
                            {
                                numbers.Add((int)Char.GetNumericValue(target.Text.ElementAt(i)));
                            }
                        }
                    }
                }
            }

            // 現在のセルに、行・列・ブロックで入っていない1から9の数字を取り出す
            for (var n = 1; n <= 9; n++)
            {
                var inputed = false;
                foreach (var num in numbers)
                {
                    if (n == num)
                    { 
                        inputed = true;
                        break;
                    }
                }
                if (!inputed)
                {
                    // 行・列・ブロックで入っていない1から9の数字が８個ある。つまり数字確定
                    var isRemove = numbers.Count == 8 ? true : false;
                    this.InputNumber(x, y, n, isRemove);
                }
            }
        }
    }
}
