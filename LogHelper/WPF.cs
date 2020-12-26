using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LogHelper
{
    public class WPF
    {
        public StartWindow _window;
        private ICommand _openCommand;

        public WPF() { }

        public void Init(StartWindow window)
        {
            _window = window;
            _openCommand = new OpenCommand();
        }

        public string GetFileName(string extension = "")
        {
            OpenFileDialog open = null;
            DoInvoke(() =>
            {
                open = new OpenFileDialog()
                {
                    AddExtension = false,
                    DefaultExt = extension,
                    Multiselect = false,
                };

                open.ShowDialog();
            });

            return open.FileName;
        }

        private void DoInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            _window.Dispatcher.Invoke(action, priority);
        }

        internal void SetAvailableReaders(ReaderDescription[] descriptions)
        {
            DoInvoke(() =>
            {
                foreach (ReaderDescription description in descriptions)
                {
                    MenuItem menuItem = new MenuItem()
                    {
                        Header = description.Label,
                        CommandParameter = description.Key,
                        Command = _openCommand,
                    };
                    menuItem.Click += _window.MenuItem_Click;
                    _window.LogTypes.Items.Add(menuItem);
                }
            });
        }

        internal void BindData(DataContainer collection)
        {
            DoInvoke(() =>
            {
                if (_window.LogList.ItemsSource == null)
                    _window.LogList.ItemsSource = collection.AllElements;
                _window.LogList.Items.Refresh();

                _window.AllElement.Children.Clear();
                foreach (KeyValuePair<string, List<LogElement>> module in collection.Module)
                    _window.AllElement.Children.Add(new LogControll(module.Key, module.Value, new Action<bool>(b => { })));
            });
        }

        private class OpenCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                string commandParameter = parameter as string;
                //_core.StartReader(commandParameter);
            }
        }
    }
}
