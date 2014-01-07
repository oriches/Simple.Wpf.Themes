Simple.Wpf.Themes
=================

A simple theme manager with optional UI user control for use in an WPF application - all you need is a set of URIs for the resource dictionaries you want to use as the themes for the application.

For more information about the releases see [Release Info] (https://github.com/oriches/Simple.Wpf.Themes/wiki/Release-Info).

Currently we support the following .Net versions:

Supported versions:

	.NET framework 4.0 and higher,
	
This library is available as a nuget [package] (https://www.nuget.org/packages/Simple.Wpf.Themes/).

You can skip the intro and go straight to the [Getting Started] (https://github.com/oriches/Simple.Wpf.Themes/wiki/Getting-Started) guide

# Introduction

The theme manager allows theme is to be applied to either the application scope or at a user control scope in the visual tree, the later means only UI content in the user control or child controls will be styled with the theme.

The theme manager is used by the theme user control, the theme control is a simple drop down list which you are resonsible for populating but when a theme is selected it is applied to specified scope. The theme control has threee dependency properties:

1. ItemsSource - binding an IEnumerable&lt;Theme&gt; to the drop down list,
2. SelectedItem - the currently selected theme,
3. Scope - the scope the theme will be applied too, this would be NULL for application scope or the user control.

Example of the these being used:

```XAML
<Grid>

    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>

    <themes:Themes x:Name="ThemesControl"
                    Grid.Row="0"
                    Margin="5"
                    Scope"{Element ElementName=StyleGrid}"
                    ItemsSource="{Binding Path=Themes,Mode=OneWay}" 
                    SelectedItem="{Binding Path=SelectedTheme, Mode=TwoWay}" />
                    
    <Grid x:Name="StyledGrid"
    	  Grid.Row="1">
    	  
    	  <TextBlock Content="Some example text...">
    	  
    </Grid>

</Grid>
```

Shown below is the library in action, you can see the Theme user control at the top - drop down list. As the user selects a different theme the styles of the controls below are updated.

![alt text](https://raw.github.com/oriches/Simple.Wpf.Themes/master/Readme%20Images/test%20harness.png "Screen shots of theme test harness")
