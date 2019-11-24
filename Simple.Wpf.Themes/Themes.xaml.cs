using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Simple.Wpf.Themes
{
    public partial class Themes
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable<Theme>),
            typeof(Themes),
            new PropertyMetadata(Enumerable.Empty<Theme>(), OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
            typeof(Theme),
            typeof(Themes),
            new PropertyMetadata(null, OnSelectedItemChanged));

        public static readonly DependencyProperty ScopeProperty = DependencyProperty.Register("Scope",
            typeof(DispatcherObject),
            typeof(Themes),
            new PropertyMetadata(null));

        public Themes()
        {
            InitializeComponent();

            ThemesComboBox.DisplayMemberPath = "Name";
            ThemesComboBox.SelectedValuePath = "Uri";
            ThemesComboBox.SelectionChanged += ThemesComboBoxOnSelectionChanged;
        }

        public IEnumerable<Theme> ItemsSource
        {
            get => (IEnumerable<Theme>) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public Theme SelectedItem
        {
            get => (Theme) GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public DispatcherObject Scope
        {
            get => (DispatcherObject) GetValue(ScopeProperty);
            set => SetValue(ScopeProperty, value);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue) return;

            if (args.OldValue != null && args.NewValue != null)
            {
                var oldEnum = (IEnumerable<Theme>) args.OldValue;
                var newEnum = (IEnumerable<Theme>) args.NewValue;

                if (oldEnum.SequenceEqual(newEnum)) return;
            }

            var themesArray = ((IEnumerable<Theme>) args.NewValue)?.ToArray() ?? new Theme[0];

            var themes = (Themes) d;
            themes.UpdateItems(themesArray);
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue) return;

            var newTheme = args.NewValue as Theme;

            var themes = (Themes) d;
            themes.UpdateTheme(newTheme);
        }

        private void ThemesComboBoxOnSelectionChanged(object sender,
            SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (Scope == null || Scope is Application)
            {
                ThemeManager.ApplyTheme((Theme) ThemesComboBox.SelectedItem);
            }
            else if (Scope != null || Scope is ContentControl)
            {
                var contentControl = (ContentControl) Scope;
                ThemeManager.ApplyTheme(contentControl, (Theme) ThemesComboBox.SelectedItem);
            }
        }

        private void UpdateItems(Theme[] themes)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            ThemesComboBox.ItemsSource = themes;
            ThemeManager.AvailableThemes = themes;

            ThemesComboBox.SelectedItem = themes.First();
        }

        private void UpdateTheme(Theme newTheme)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            var existingTheme = ThemeManager.AvailableThemes.SingleOrDefault(x => x.Name == newTheme.Name);
            ThemesComboBox.SelectedItem = existingTheme;
        }
    }
}