using Microsoft.Extensions.Configuration;
using System;
using System.Windows;

namespace LogHelper
{
    /// <summary>
    /// Корень приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            try
            {
                // Зависимости
                IConfiguration configuration = Conficurated();
                WorkWindow workWindow = new WorkWindow();
                WPF adapter = new WPF(workWindow);
                DataContainer dataContainer = new DataContainer();
                ReaderFactory factory = new ReaderFactory(configuration.GetSection("Readers"));
                Core core = new Core(adapter, dataContainer, factory);
                workWindow.BindCallBack(core);

                Hide();
                workWindow.Show();

            }
            catch (Exception ex)
            {
                Logger.Log(ex, "");
            }
            Close();
        }

        public IConfiguration Conficurated()
        {
           return new ConfigurationBuilder()
                .SetBasePath(@"I:\Settings")
                .AddJsonFile("LogHelper.json")
                .Build();
        }
    }
}
