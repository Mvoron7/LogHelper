using System.Windows;
using System.Windows.Controls;

namespace LogHelper
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly Core _core;

        public StartWindow(Core core)
        {
            InitializeComponent();

            _core = core;
            _core.Init(this);
        }

        internal void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem a = sender as MenuItem;
            _core.StartReader(a.CommandParameter as string);
        }
    }
}
