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

        private readonly IEnumerable<LogElement> _elements;
        private Action<bool> _bindCommand;

        public LogControll(string message, IEnumerable<LogElement> elements, Action<bool> action)
        {
            InitializeComponent();

            Text.Content = message;
            _elements = elements;
            _bindCommand = action;
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e) => _bindCommand?.Invoke(true);

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e) => _bindCommand?.Invoke(false);
    }
}
