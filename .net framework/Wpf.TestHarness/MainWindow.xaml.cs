using System;
using Simple.Wpf.Themes.Common;

namespace Wpf.TestHarness
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var themes = new[]
            {
                new Theme("Default theme",
                    new Uri("/Wpf.TestHarness;component/Themes/DefaultTheme.xaml", UriKind.Relative)),
                new Theme("Red theme", new Uri("/Wpf.TestHarness;component/Themes/RedTheme.xaml", UriKind.Relative)),
                new Theme("Blue theme", new Uri("/Wpf.TestHarness;component/Themes/BlueTheme.xaml", UriKind.Relative))
            };

            ThemesControl.ItemsSource = themes;
        }
    }
}