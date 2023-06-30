using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NumberKeyEventHandler numberKeyEventHandler = new NumberKeyEventHandler();
        private CommandKeyEventHandler commandKeyEventHandler = new CommandKeyEventHandler();
        private CheckKeyEventHandler checkKeyEventHandler = new CheckKeyEventHandler();
        private ColorKeyEventHandler colorKeyEventHandler = new ColorKeyEventHandler();
        private SeekKeyEventHandler seekKeyEventHandler = new SeekKeyEventHandler();

        public MainWindow()
        {
            InitializeComponent();
            numberKeyEventHandler.MainWindow = this;
            commandKeyEventHandler.MainWindow = this;
            checkKeyEventHandler.MainWindow = this;
            seekKeyEventHandler.MainWindow = this;
            colorKeyEventHandler.MainWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += seekKeyEventHandler.KeyDown;
            window.KeyDown += colorKeyEventHandler.KeyDown;
            window.KeyDown += numberKeyEventHandler.KeyDown;
            window.KeyDown += commandKeyEventHandler.KeyDown;
            window.KeyDown += checkKeyEventHandler.KeyDown;
        }

    }
}
