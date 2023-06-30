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
    internal class CheckKeyEventHandler : KeyEventHandlerBase
    {
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                // 数字チェック
                var result = this.CheckNumber();
                if (result)
                {
                    this.MainWindow.debug.Text = "OK!";
                }
                else
                {
                    this.MainWindow.debug.Text = "NG!";
                }
            }
        }

        private bool CheckNumber()
        {
            var totals = new HashSet<int>();

            // 各列の合計
            for (var counterX = 0; counterX < 9; counterX++)
            {
                var total = 0;
                for (var counterY = 0; counterY < 9; counterY++)
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
                                total += (int)Char.GetNumericValue(target.Text.ElementAt(i));
                            }
                        }
                    }
                }
                totals.Add(total);
            }

            // 各行の合計
            for (var counterY = 0; counterY < 9; counterY++)
            {
                var total = 0;
                for (var counterX = 0; counterX < 9; counterX++)
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
                                total += (int)Char.GetNumericValue(target.Text.ElementAt(i));
                            }
                        }
                    }
                }
                totals.Add(total);
            }

            // ブロックの合計
            for (var targetX = 0; targetX < 3; targetX++)
            {
                for (var targetY = 0; targetY < 3; targetY++)
                {
                    var total = 0;
                    for (var counterX = targetX * 3; counterX < targetX * 3 + 3; counterX++)
                    {
                        for (var counterY = targetY * 3; counterY < targetY * 3 + 3; counterY++)
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
                                        total += (int)Char.GetNumericValue(target.Text.ElementAt(i));
                                    }
                                }
                            }
                        }
                    }
                    totals.Add(total);
                }
            }
            // 合計は45だけのはず
            if (totals.Count == 1 && totals.FirstOrDefault() == 45)
            {
                return true;
            }

            return false;
        }
    }
}
