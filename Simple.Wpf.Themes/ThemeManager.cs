namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public static class ThemeManager
    {
        private static readonly IDictionary<DispatcherObject, ResourceDictionary> CurrentThemes;

        static ThemeManager()
        {
            CurrentThemes = new Dictionary<DispatcherObject, ResourceDictionary>();
        }

        public static IEnumerable<Theme> AvailableThemes { get; set; }

        public static void ApplyTheme(Theme theme)
        {
            Contract.Requires<ArgumentNullException>(theme != null);

            ApplyThemeImpl(Application.Current, Application.Current.Resources, theme);
        }

        public static void ApplyTheme(ContentControl control, Theme theme)
        {
            Contract.Requires<ArgumentNullException>(control != null);
            Contract.Requires<ArgumentNullException>(theme != null);

            ApplyThemeImpl(control, control.Resources, theme);
        }

        private static void ApplyThemeImpl(DispatcherObject @object, ResourceDictionary resources, Theme theme)
        {
            Contract.Requires<ArgumentNullException>(theme != null);
            Contract.Requires<ArgumentNullException>(resources != null);

            ResourceDictionary current;
            if (CurrentThemes.TryGetValue(@object, out current))
            {
                resources.MergedDictionaries.Remove(current);

            }

            if (theme.Uri != null)
            {
                var resourceDictionary = Application.LoadComponent(theme.Uri) as ResourceDictionary;
                if (resourceDictionary != null)
                {
                    resources.MergedDictionaries.Add(resourceDictionary);
                    CurrentThemes[@object] = resourceDictionary;
                }
            }
        }
    }
}