namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public partial class Themes : UserControl
    {

        /// <summary>
        /// The items to be applied to the theme manager.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable<Theme>),
            typeof(Themes),
            new PropertyMetadata(Enumerable.Empty<Theme>(), OnItemsSourceChanged));

        /// <summary>
        /// The currently selected theme.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
           typeof(Theme),
           typeof(Themes),
           new PropertyMetadata(null, OnSelectedItemChanged));

        /// <summary>
        /// The scope the theme will be applied too.
        /// </summary>
        public static readonly DependencyProperty ScopeProperty = DependencyProperty.Register("Scope",
             typeof(DispatcherObject),
             typeof(Themes),
             new PropertyMetadata(null, OnScopeChanged));

        /// <summary>
        /// Default constructor
        /// </summary>
        public Themes()
        {
            InitializeComponent();

            ThemesComboBox.DisplayMemberPath = "Name";
            ThemesComboBox.SelectedValuePath = "Uri";
            ThemesComboBox.SelectionChanged += ThemesComboBoxOnSelectionChanged;
        }


        /// <summary>
        /// The bound items to the themes.
        /// </summary>
        public IEnumerable<Theme> ItemsSource
        {
            get { return (IEnumerable<Theme>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// The currently selected theme.
        /// </summary>
        public Theme SelectedItem
        {
            get { return (Theme)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// The scope the theme will be applied too.
        /// </summary>
        public DispatcherObject Scope
        {
            get { return (DispatcherObject)GetValue(ScopeProperty); }
            set { SetValue(ScopeProperty, value); }
        }

        private static void OnScopeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue)
            {
                return;
            }
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

            var themesArray = newEnum.ToArray();

            var themes = (Themes)d;
            themes.UpdateItems(themesArray);
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue)
            {
                return;
            }

            var newTheme = args.NewValue as Theme;

            var themes = (Themes)d;
            themes.UpdateTheme(newTheme);
        }

        private void ThemesComboBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (Scope == null || Scope is Application)
            {
                ThemeManager.ApplyTheme((Theme)ThemesComboBox.SelectedItem);
            }
            else if (Scope != null || Scope is ContentControl)
            {
                var contentControl = (ContentControl)Scope;
                ThemeManager.ApplyTheme(contentControl, (Theme)ThemesComboBox.SelectedItem);
            }
        }

        private void UpdateItems(Theme[] themes)
        {
            Contract.Requires<ArgumentNullException>(themes != null);

            ThemesComboBox.ItemsSource = themes;
            ThemeManager.AvailableThemes = themes;

            ThemesComboBox.SelectedItem = themes.First();
        }

        private void UpdateTheme(Theme newTheme)
        {
            ThemesComboBox.SelectedItem = null;

            var existingTheme = ThemeManager.AvailableThemes.SingleOrDefault(x => x.Name == newTheme.Name);
            if (existingTheme != null)
            {
                ThemesComboBox.SelectedItem = existingTheme;
            }
        }
    }
}
