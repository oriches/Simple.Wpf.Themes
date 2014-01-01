namespace Wpf.TestHarness
{
    using System;
    using System.Windows;
    using Simple.Wpf.Themes;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var themes = new[]
            {
                new Theme("No theme (default)", null),
                new Theme("Red theme", new Uri("/Wpf.TestHarness;component/Themes/RedTheme.xaml", UriKind.Relative)),
                new Theme("Blue theme", new Uri("/Wpf.TestHarness;component/Themes/BlueTheme.xaml", UriKind.Relative))
            };

            ThemesControl.ItemsSource = themes;
        }
    }
}
