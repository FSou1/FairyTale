namespace FT.Components.Extension {
    public static class StringExtension {
        public static string Truncate(this string s, int length) {
            if (s.Length > length) return s.Substring(0, length);
            return s;
        }
    }
}