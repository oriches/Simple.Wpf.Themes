namespace Wpf.Mvvm.TestHarness
{
    public partial class App
    {
        public App()
        {
            var mainWindow = new MainWindow {DataContext = new MainViewModel()};

            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}