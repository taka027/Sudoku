using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace SudokuV3
{
    internal class NumberKeyEventHandler : KeyEventHandlerBase
    {
        public void KeyDown(object sender, KeyEventArgs e)
        {
            // 一旦消す（白塗）
            EventUtility.Paint(this.MainWindow, x, y, Brushes.White);

            if (e.Key == Key.K)
            {
                y--;
                if (y < 0)
                {
                    y = 8;
                }
            }
            if (e.Key == Key.J)
            {
                y++;
                if (y > 8)
                {
                    y = 0;
                }
            }
            if (e.Key == Key.H)
            {
                x--;
                if (x < 0)
                {
                    x = 8;
                }
            }
            if (e.Key == Key.L)
            {
                x++;
                if (x > 8)
                {
                    x = 0;
                }
            }

            this.MainWindow.debug.Text = "(" + x + "," + y + ")";

            // 今の位置で色を塗る
            EventUtility.Paint(this.MainWindow, x, y, Brushes.SkyBlue);

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                if (System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift
                    && System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Control)
                {
                    // シフトキーが押されていないとき
                    // 数字を入れる
                    int num = (char)e.Key - 34;
                    this.InputNumber(x, y, num);
                }
            }
        }
    }
}
