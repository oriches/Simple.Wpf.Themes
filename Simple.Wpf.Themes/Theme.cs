namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Defines a theme as a Name &amp; a URI to the ResourceDictionary
    /// </summary>
    public sealed class Theme : IEquatable<Theme>
    {
        /// <summary>
        /// The humna readable name of the theme
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The URI of the theme ResourceDictionary
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Create a theme class
        /// </summary>
        /// <param name="name">The name of theme.</param>
        /// <param name="uri">The URI of the ResourceDictionary.</param>
        public Theme(string name, Uri uri)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            Name = name;
            Uri = uri;
        }

        /// <summary>
        /// Determines whether two themes instances are equal.
        /// </summary>
        /// <param name="other">The theme to compare with the current theme.</param>
        /// <returns>true if the specified theme is equal to the current theme; otherwise, false.</returns>
        public bool Equals(Theme other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        /// <summary>
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current theme.</param>
        /// <returns>true if the specified object is equal to the current theme; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Theme && Equals((Theme)obj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current theme.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// The equality operator (==) returns true if its operands are equal, false otherwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Theme left, Theme right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// The inequality operator (!=) returns false if its operands are equal, true otherwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Theme left, Theme right)
        {
            return !Equals(left, right);
        }

        private sealed class NameEqualityComparer : IEqualityComparer<Theme>
        {
            public bool Equals(Theme x, Theme y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name);
            }

            public int GetHashCode(Theme obj)
            {
                return (obj.Name != null ? obj.Name.GetHashCode() : 0);
            }
        }

        private static readonly IEqualityComparer<Theme> NameComparerInstance = new NameEqualityComparer();

        /// <summary>
        /// Theme equality comparer/
        /// </summary>
        public static IEqualityComparer<Theme> NameComparer
        {
            get { return NameComparerInstance; }
        }
    }
}