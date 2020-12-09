using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LogHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Core _core;

        public MainWindow()
        {
            InitializeComponent();

            _core = new Core();
            _core.Init(this);
        }

        internal void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var a = sender as MenuItem;
            _core.StartReader(a.CommandParameter as string);
        }
    }
}
