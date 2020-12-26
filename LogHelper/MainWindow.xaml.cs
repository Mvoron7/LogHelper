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
            Core _core = new Core();
            StartWindow _startWindow = new StartWindow(_core);
            Hide();
            _startWindow.Show();
            Close();
        }
    }
}
