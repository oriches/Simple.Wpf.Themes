namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    /// <summary>
    /// Theme manager for an WPF application.
    /// </summary>
    public static class ThemeManager
    {
        private static readonly IDictionary<DispatcherObject, ResourceDictionary> CurrentThemes;

        static ThemeManager()
        {
            CurrentThemes = new Dictionary<DispatcherObject, ResourceDictionary>();
        }

        /// <summary>
        /// The available themes to be applied to a content control or the whole application.
        /// </summary>
        public static IEnumerable<Theme> AvailableThemes { get; set; }

        /// <summary>
        /// Applies a theme to the application.
        /// </summary>
        /// <param name="theme">The theme to be applied.</param>
        public static void ApplyTheme(Theme theme)
        {
            var localThemes = AvailableThemes ?? Enumerable.Empty<Theme>();
            if (localThemes.All(x => x != theme))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplyThemeImpl(Application.Current, Application.Current.Resources, theme);
        }

        /// <summary>
        /// Applies a theme to a content control
        /// </summary>
        /// <param name="control">The control the theme will be applied to.</param>
        /// <param name="theme">The theme to be applied</param>
        public static void ApplyTheme(ContentControl control, Theme theme)
        {
            Contract.Requires<ArgumentNullException>(control != null);

            var localThemes = AvailableThemes ?? Enumerable.Empty<Theme>();
            if (localThemes.All(x => x != theme))
            {
                throw new ArgumentException("Unknown theme!");
            }

            ApplyThemeImpl(control, control.Resources, theme);
        }

        private static void ApplyThemeImpl(DispatcherObject @object, ResourceDictionary resources, Theme theme)
        {
            Contract.Requires<ArgumentNullException>(resources != null);

            ResourceDictionary current;
            if (CurrentThemes.TryGetValue(@object, out current))
            {
                resources.MergedDictionaries.Remove(current);

            }

            if (theme != null && theme.Uri != null)
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