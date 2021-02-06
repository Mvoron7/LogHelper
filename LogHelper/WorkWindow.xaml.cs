using LogHelper.Abstraction;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LogHelper
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        private ICallBack _callBack;

        public WorkWindow()
        {
            InitializeComponent();
        }

        public void BindCallBack(ICallBack callBack)
        {
            _callBack = callBack;
        }

        internal void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem a = sender as MenuItem;
            _callBack.StartReader(a.CommandParameter as string);
        }

        #region Filters&Converters
        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            LogElement element = e.Item as LogElement;
            if (element != null)
                e.Accepted = element.Visible;
        }
        #endregion
    }
}
