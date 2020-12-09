using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LogHelper
{
    public class WPF
    {
        public MainWindow _window;
        private ICommand _openCommand;

        public WPF(){ }

        public void Init(MainWindow window)
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
