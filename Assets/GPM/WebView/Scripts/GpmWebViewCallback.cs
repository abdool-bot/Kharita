namespace Gpm.WebView
{
    public static class GpmWebViewCallback
    {
        public enum CallbackType
        {
            Open,
            Close,
            PageLoad,
            MultiWindowOpen,
            MultiWindowClose,
            Scheme,
            GoBack,
            GoForward,
            ExecuteJavascript
        }

        public delegate void GpmWebViewDelegate(CallbackType type, string data, GpmWebViewError error);

        [System.Obsolete("This delegate is deprecated.")]
        public delegate void GpmWebViewErrorDelegate(GpmWebViewError error);
        [System.Obsolete("This delegate is deprecated.")]
        public delegate void GpmWebViewDelegate<T>(T data, GpmWebViewError error);
        [System.Obsolete("This delegate is deprecated.")]
        public delegate void GpmWebViewPageLoadDelegate(string url);
    }
}
