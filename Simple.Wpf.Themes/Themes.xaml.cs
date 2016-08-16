namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    /// <summary>
    /// The UI control for binding &amp; selecting a theme in an application.
    /// </summary>
    public partial class Themes
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
             new PropertyMetadata(null));

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Themes()
        {
            Contract.ContractFailed += HandleContractFailed;

            InitializeComponent();

            ThemesComboBox.DisplayMemberPath = "Name";
            ThemesComboBox.SelectedValuePath = "Uri";
            ThemesComboBox.SelectionChanged += ThemesComboBoxOnSelectionChanged;
        }

        /// <summary>
        /// The bound items to the themes control, this will be of type Theme.
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
        /// The scope the selected theme will be applied too, this could be the application or a content control.
        /// </summary>
        public DispatcherObject Scope
        {
            get { return (DispatcherObject)GetValue(ScopeProperty); }
            set { SetValue(ScopeProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue == args.NewValue)
            {
                return;
            }

            if (args.OldValue != null && args.NewValue != null)
            {
                var oldEnum = (IEnumerable<Theme>)args.OldValue;
                var newEnum = (IEnumerable<Theme>)args.NewValue;

                if (oldEnum.SequenceEqual(newEnum))
                {
                    return;
                }
            }

            var themesArray = ((IEnumerable<Theme>) args.NewValue)?.ToArray() ?? new Theme[0];

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

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            ThemesComboBox.ItemsSource = themes;
            ThemeManager.AvailableThemes = themes;

            ThemesComboBox.SelectedItem = themes.First();
        }
        
        private void UpdateTheme(Theme newTheme)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            var existingTheme = ThemeManager.AvailableThemes.SingleOrDefault(x => x.Name == newTheme.Name);
            ThemesComboBox.SelectedItem = existingTheme;
        }

        private void HandleContractFailed(object sender, ContractFailedEventArgs args)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                args.SetHandled();
            }
        }
    }
}
