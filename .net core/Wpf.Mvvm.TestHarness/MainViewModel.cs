using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Simple.Wpf.Themes.Common;

namespace Wpf.Mvvm.TestHarness
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private Theme _selectedTheme;

        public MainViewModel()
        {
            Themes = new[]
            {
                new Theme("Red theme",
                    new Uri("/Wpf.Mvvm.TestHarness;component/Themes/RedTheme.xaml", UriKind.Relative)),
                new Theme("Blue theme",
                    new Uri("/Wpf.Mvvm.TestHarness;component/Themes/BlueTheme.xaml", UriKind.Relative))
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}