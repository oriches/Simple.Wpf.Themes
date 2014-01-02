Simple.Wpf.Themes
=================

A simple theme manager with optional UI user control for use in an WPF application - all you need is a set of URIs for the resource dictionaries you want to use as the themes for the application.

The repo contains 2 test harnesses, one code-behind and the other an MVVM implementation, these are detailed below.

## Code behind implementation
This example creates an array of Theme objects in the constructor of the code behind for the main window and this is then set to the ItemsSource of the Theme user control. The user control uses a static ThemeManager class internally to manipulate the current theme.

```
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
```

XAML:

```
<Grid>
        
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
        
    <themes:Themes x:Name="ThemesControl"
                    Grid.Row="0"
                    Margin="5"/>
        
</Grid>
```

## MVVM implementation
This example creates the array of Theme objects in the ViewModel, the Themes are bound in the XAML:

```
public sealed class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Theme _selectedTheme;

    public MainViewModel()
    {
        Themes = new[]
                    {
                        new Theme("No theme (default)", null),
                        new Theme("Red theme", new Uri("/Wpf.Mvvm.TestHarness;component/Themes/RedTheme.xaml", UriKind.Relative)),
                        new Theme("Blue theme", new Uri("/Wpf.Mvvm.TestHarness;component/Themes/BlueTheme.xaml", UriKind.Relative))
                    };

        _selectedTheme = Themes.First();
    }

    public IEnumerable<Theme> Themes { get; private set; }

    public Theme SelectedTheme
    {
        get
        {
            return _selectedTheme;
        }
        set
        {
            _selectedTheme = value;
            OnPropertyChanged("SelectedTheme");
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```

XAML:

```
<Grid>

    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>

    <themes:Themes x:Name="ThemesControl"
                    Grid.Row="0"
                    Margin="5"
                    ItemsSource="{Binding Path=Themes,Mode=OneWay}" 
                    SelectedItem="{Binding Path=SelectedTheme, Mode=TwoWay}"/>

</Grid>
```
