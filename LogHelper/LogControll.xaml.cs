using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace LogHelper
{
    /// <summary>
    /// Визуальное отображение фильтра элементов.
    /// </summary>
    public partial class LogControll : UserControl
    {

        public readonly List<LogElement> Elements;
        private Action<bool> _bindCommand;

        public LogControll(string message, List<LogElement> elements, Action<bool> action)
        {
            InitializeComponent();

            Text.Content = message;
            Elements = elements;
            _bindCommand = action;
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e) => _bindCommand(true);

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e) => _bindCommand(false);
    }
}
