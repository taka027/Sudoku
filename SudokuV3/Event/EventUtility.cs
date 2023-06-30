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
    internal class EventUtility
    {
        public static void Paint(MainWindow mainWindow, int x, int y, SolidColorBrush brush)
        {
            Type type = mainWindow.GetType();
            var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + x + "_" + y));
            if (field != null)
            {
                var target = field.GetValue(mainWindow);
                if (target != null)
                {
                    ((System.Windows.Controls.TextBlock)target).Background = brush;
                }
            }
        }

        public static void Paint(MainWindow mainWindow, int x, int y, SolidColorBrush brush1, SolidColorBrush brush2, SolidColorBrush brush3)
        {
            Type type = mainWindow.GetType();
            var field = type.GetRuntimeFields().FirstOrDefault(f => f.Name.Contains("innerText" + x + "_" + y));
            if (field != null)
            {
                var target = field.GetValue(mainWindow);
                if (target != null)
                {
                    if (((System.Windows.Controls.TextBlock)target).Background == brush1)
                    {
                        // もしすでに第一の色のときは第二の色にする
                        ((System.Windows.Controls.TextBlock)target).Background = brush2;
                    }
                    else if (((System.Windows.Controls.TextBlock)target).Background == brush2)
                    {
                        // もしすでに第二の色のときは第三の色にする
                        ((System.Windows.Controls.TextBlock)target).Background = brush3;
                    }
                    else if (((System.Windows.Controls.TextBlock)target).Background == brush3)
                    {
                        // もしすでに第三の色のときは第三の色にする
                        ((System.Windows.Controls.TextBlock)target).Background = brush3;
                    }
                    else
                    {
                        // もし白色のときは第一の色にする
                        ((System.Windows.Controls.TextBlock)target).Background = brush1;
                    }
                }
            }
        }
    }
}
