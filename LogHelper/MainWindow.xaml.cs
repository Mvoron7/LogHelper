using System.Windows;

namespace LogHelper
{
    /// <summary>
    /// Корень приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        // Эти строки должны получаться из файла настроек как объект .
        private readonly string[] directors = { "File", "Тестовый", "<date[ ]?=[ ]?\"([\\d]{4}-[\\d]{2}-[\\d]{2} [\\d]{2}:[\\d]{2}:[\\d]{2}.[\\d]{4})\"[ ]?Tag[ ]?=[ ]?\"([^\"]*)\"[ ]?Message[ ]?=[ ]?\"([^\"]*)\"[ ]?>", "Log File|*.log" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, System.EventArgs e)
        {
            // Зависимости 
            WorkWindow workWindow = new WorkWindow();
            WPF adapter = new WPF(workWindow);
            DataContainer dataContainer = new DataContainer();
            Core core = new Core(adapter, dataContainer, directors);
            workWindow.BindCallBack(core);

            Hide();
            workWindow.Show();
            Close();
        }
    }
}
