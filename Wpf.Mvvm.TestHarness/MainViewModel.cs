namespace Wpf.Mvvm.TestHarness
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Simple.Wpf.Themes;

    public sealed class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Theme _selectedTheme;

        public MainViewModel()
        {
            Themes = new[]
                     {
                         new Theme("Red theme", new Uri("/Wpf.Mvvm.TestHarness;component/Themes/RedTheme.xaml", UriKind.Relative)),
                         new Theme("Blue theme", new Uri("/Wpf.Mvvm.TestHarness;component/Themes/BlueTheme.xaml", UriKind.Relative))
                     };

            _selectedTheme = Themes.First();
        }

        public IEnumerable<Theme> Themes { get; }

        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                OnPropertyChanged("SelectedTheme");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}