using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    internal class KeyEventHandlerBase
    {
        public MainWindow MainWindow { get; set; }

        public static int x = 0;
        public static int y = 0;
        public static bool isColoring = false;

        protected string InputNumber(int x, int y, int number, bool isRemove = true)
        {
            var now = string.Empty;
            Type type = this.MainWindow.GetType();
            var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + x + "_" + y));
            if (field != null)
            {
                var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                if (target != null)
                {
                    if (number == 0)
                    {
                        // 0のときはセル内を強制的にクリアする
                        target.Text = string.Empty;
                        target.FontSize = 18;
                        target.LineHeight = 18;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(target.Text))
                        {
                            // 何か入っているとき
                            if (target.Text.Contains(number.ToString()))
                            {
                                // 今入れようとしている数字が入っているとき
                                var sb = new StringBuilder(target.Text);
                                // 取り除く
                                sb.Replace(number.ToString(), "");
                                target.Text = sb.ToString();
                                target.FontSize = 18;
                                target.LineHeight = 18;

                                // 数字がひとつになったら
                                if (target.Text.Length == 1)
                                {
                                    target.FontSize = 58;
                                    target.LineHeight = 58;
                                    if (isRemove)
                                    {
                                        // 行、列、ブロックの数字メモからこの数字を外す
                                        this.RemoveNumber(x, y, int.Parse(target.Text));
                                    }
                                }
                            }
                            else
                            {
                                // 追加する
                                target.Text = target.Text + number.ToString();
                                target.FontSize = 18;
                                target.LineHeight = 18;
                            }
                        }
                        else
                        {
                            // 入っていないとき
                            target.Text = number.ToString();
                            target.FontSize = 58;
                            target.LineHeight = 58;
                            if (isRemove)
                            {
                                // 行、列、ブロックの数字メモからこの数字を外す
                                this.RemoveNumber(x, y, number);
                            }
                        }
                    }
                    now = target.Text;
                }
            }

            return now;
        }

        private void RemoveNumber(int x, int y, int number)
        {
            // 現在の行のすべてのセルから入っている数字を全種類取り出す
            for (var counterX = 0; counterX < 9; counterX++)
            {
                if (counterX == x)
                {
                    continue;
                }
                Type type = this.MainWindow.GetType();
                var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + y));
                if (field != null)
                {
                    var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                    if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length > 1)
                    {
                        // 数字メモとして入っているとき
                        var sb = new StringBuilder(target.Text);
                        // 取り除く
                        sb.Replace(number.ToString(), "");
                        target.Text = sb.ToString();
                        target.FontSize = 18;
                        target.LineHeight = 18;

                        // 数字がひとつになったら
                        if (target.Text.Length == 1)
                        {
                            target.FontSize = 58;
                            target.LineHeight = 58;
                            // 行、列、ブロックの数字メモからこの数字を外す
                            this.RemoveNumber(counterX, y, int.Parse(target.Text));
                        }
                    }
                }
            }

            // 現在の列のすべてのセルから入っている数字を全種類取り出す
            for (var counterY = 0; counterY < 9; counterY++)
            {
                if (counterY == y)
                {
                    continue;
                }
                Type type = this.MainWindow.GetType();
                var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + x + "_" + counterY));
                if (field != null)
                {
                    var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                    if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length > 1)
                    {
                        // 数字メモとして入っているとき
                        var sb = new StringBuilder(target.Text);
                        // 取り除く
                        sb.Replace(number.ToString(), "");
                        target.Text = sb.ToString();
                        target.FontSize = 18;
                        target.LineHeight = 18;

                        // 数字がひとつになったら
                        if (target.Text.Length == 1)
                        {
                            target.FontSize = 58;
                            target.LineHeight = 58;
                            // 行、列、ブロックの数字メモからこの数字を外す
                            this.RemoveNumber(x, counterY, int.Parse(target.Text));
                        }
                    }
                }
            }

            // 現在のブロックのすべてのセルから入っている数字を全種類取り出す
            for (var counterX = (x / 3) * 3; counterX < (x / 3) * 3 + 3; counterX++)
            {
                if (counterX == x)
                {
                    continue;
                }
                for (var counterY = (y / 3) * 3; counterY < (y / 3) * 3 + 3; counterY++)
                {
                    if (counterY == y)
                    {
                        continue;
                    }
                    Type type = this.MainWindow.GetType();
                    var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + counterX + "_" + counterY));
                    if (field != null)
                    {
                        var target = (System.Windows.Controls.TextBlock?)field.GetValue(this.MainWindow);
                        if (target != null && !string.IsNullOrEmpty(target.Text) && target.Text.Length > 1)
                        {
                            // 数字メモとして入っているとき
                            var sb = new StringBuilder(target.Text);
                            // 取り除く
                            sb.Replace(number.ToString(), "");
                            target.Text = sb.ToString();
                            target.FontSize = 18;
                            target.LineHeight = 18;

                            // 数字がひとつになったら
                            if (target.Text.Length == 1)
                            {
                                target.FontSize = 58;
                                target.LineHeight = 58;
                                // 行、列、ブロックの数字メモからこの数字を外す
                                this.RemoveNumber(counterX, counterY, int.Parse(target.Text));
                            }
                        }
                    }
                }
            }
        }
    }
}
