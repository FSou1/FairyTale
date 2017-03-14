namespace FT.MvcApp.Shared.Models
{
    public class LayoutViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CanonicalUrl { get; set; }

        public bool HasDescription => !string.IsNullOrEmpty(Description);
        public bool HasCanonical => !string.IsNullOrEmpty(CanonicalUrl);

        #if DEBUG
        public bool IncludeMetrics => false;
        #else
        public bool IncludeMetrics => true;
        #endif
    }
}