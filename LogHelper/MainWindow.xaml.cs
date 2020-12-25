using System.Threading;
using System.Windows;

namespace LogHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, System.EventArgs e)
        {
            new Thread(new ThreadStart(() => {
                Core _core = new Core();

                Dispatcher.Invoke(() => {
                    StartWindow _startWindow = new StartWindow(_core);
                    Hide();
                    _startWindow.Show();
                    Close();
                });
            })).Start();
        }
    }
}
