using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Simple.Wpf.Themes.Common;

namespace Simple.Wpf.Themes
{
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
            var localThemes = AvailableThemes ?? Enumerable.Empty<Theme>();
            if (localThemes.All(x => x != theme))
                throw new ArgumentException("Unknown theme!");

            ApplyThemeImpl(Application.Current, Application.Current.Resources, theme);
        }

        public static void ApplyTheme(ContentControl control, Theme theme)
        {
            var localThemes = AvailableThemes ?? Enumerable.Empty<Theme>();
            if (localThemes.All(x => x != theme))
                throw new ArgumentException("Unknown theme!");

            ApplyThemeImpl(control, control.Resources, theme);
        }

        private static void ApplyThemeImpl(DispatcherObject @object, ResourceDictionary resources, Theme theme)
        {
            if (CurrentThemes.TryGetValue(@object, out var current))
                resources.MergedDictionaries.Remove(current);

            if (theme != null && theme.Uri != null)
                if (Application.LoadComponent(theme.Uri) is ResourceDictionary resourceDictionary)
                {
                    resources.MergedDictionaries.Add(resourceDictionary);
                    CurrentThemes[@object] = resourceDictionary;
                }
        }
    }
}