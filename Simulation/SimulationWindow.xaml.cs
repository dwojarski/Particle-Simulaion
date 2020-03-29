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
using System.Windows.Shapes;

namespace Simulation
{
    /// <summary>
    /// Logic of SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        private TimeFrameManager frame;
        private bool running;
        public SimulationWindow()
        {
            InitializeComponent();
            Layer layer = new Layer(450, new Vector2d(0, 0));
            frame = new TimeFrameManager(0.03, Board, layer, this);
            frame.Start();
            this.running = true;
            frame.CreateParicles();
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            frame.Save();
        }

        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            frame.Load();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            if (running)
                frame.Stop();
            this.Close();
        }

        private void MenuStart_Click(object sender, RoutedEventArgs e)
        {
            if (!running)
                frame.Start();
                running = !running;
        }

        private void MenuPause_Click(object sender, RoutedEventArgs e)
        {
            if (running)
                frame.Stop();
                running = !running;
        }

        private void Window_Exit(object sender, EventArgs e)
        {
            this.frame.Stop();
        }
    }
}
