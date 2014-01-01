namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class Themes : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable<Theme>),
            typeof(Themes),
            new PropertyMetadata(Enumerable.Empty<Theme>(), OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
           typeof(Theme),
           typeof(Themes),
           new PropertyMetadata(null, OnSelectedItemChanged));

        public Themes()
        {
            InitializeComponent();

            ThemesComboBox.DisplayMemberPath = "Name";
            ThemesComboBox.SelectedValuePath = "Uri";
            ThemesComboBox.SelectionChanged += ThemesComboBoxOnSelectionChanged;
        }

        public IEnumerable<Theme> ItemsSource
        {
            get { return (IEnumerable<Theme>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public Theme SelectedItem
        {
            get { return (Theme)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue)
            {
                return;
            }

            var oldEnum = args.OldValue as IEnumerable<Theme>;
            var newEnum = args.NewValue as IEnumerable<Theme>;

            if (oldEnum.SequenceEqual(newEnum))
            {
                return;
            }

            var themes = (Themes)d;
            themes.UpdateItems(newEnum);
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
        }

        private void UpdateItems(IEnumerable<Theme> newEnum)
        {
            ThemesComboBox.ItemsSource = newEnum;
            ThemeManager.AvailableThemes = newEnum;

            ThemesComboBox.SelectedItem = newEnum.First();
        }

        private void ThemesComboBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            ThemeManager.ApplyTheme((Theme)ThemesComboBox.SelectedItem);
        }
    }
}
