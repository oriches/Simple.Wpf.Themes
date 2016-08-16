﻿namespace Wpf.Mvvm.TestHarness
{
    using System.Windows;

    public partial class App
    {
        public App()
        {
            var mainWindow = new MainWindow { DataContext = new MainViewModel() };

            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
