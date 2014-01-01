namespace Simple.Wpf.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public sealed class Theme : IEquatable<Theme>
    {
        public string Name { get; private set; }

        public Uri Uri { get; private set; }

        public Theme(string name, Uri uri)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            Name = name;
            Uri = uri;
        }

        public bool Equals(Theme other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Theme && Equals((Theme)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Theme left, Theme right)
        {
            return Equals(left, right);
        }

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

        public static IEqualityComparer<Theme> NameComparer
        {
            get { return NameComparerInstance; }
        }
    }
}