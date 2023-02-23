namespace Gpm.WebView.Internal
{
    using System.Collections.Generic;

    public class WebViewImplementation
    {
        private static readonly WebViewImplementation instance = new WebViewImplementation();

        public static WebViewImplementation Instance
        {
            get { return instance; }
        }

        private IWebView webview;

        private WebViewImplementation()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            webview = new AndroidWebView();
#elif UNITY_IPHONE && !UNITY_EDITOR
            webview = new IOSWebView();
#else
            webview = new DefaultWebView();
#endif
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            webview.ShowUrl(url, configuration, openCallback, closeCallback, schemeList, schemeEvent, pageLoadCallback);
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowHtmlFile(
            string filePath,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            webview.ShowHtmlFile(filePath, configuration, openCallback, closeCallback, schemeList, schemeEvent, pageLoadCallback);
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowHtmlString(
            string htmlString,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            webview.ShowHtmlString(htmlString, configuration, openCallback, closeCallback, schemeList, schemeEvent, pageLoadCallback);
        }

        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            webview.ShowUrl(url, configuration, callback, schemeList);
        }

        public void ShowHtmlFile(
            string filePath,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            webview.ShowHtmlFile(filePath, configuration, callback, schemeList);
        }

        public void ShowHtmlString(
            string htmlString,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            webview.ShowHtmlString(htmlString, configuration, callback, schemeList);
        }

        public void ShowSafeBrowsing(
            string url,
            GpmWebViewRequest.ConfigurationSafeBrowsing configuration = null,
            GpmWebViewCallback.GpmWebViewDelegate callback = null)
        {
            webview.ShowSafeBrowsing(url, configuration, callback);
        }

        public void Close()
        {
            webview.Close();
        }

        public bool IsActive()
        {
            return webview.IsActive();
        }

        public void ExecuteJavaScript(string script)
        {
            webview.ExecuteJavaScript(script);
        }

        public void SetFileDownloadPath(string path)
        {
            webview.SetFileDownloadPath(path);
        }

        public bool CanGoBack()
        {
            return webview.CanGoBack;
        }

        public bool CanGoForward()
        {
            return webview.CanGoForward;
        }

        public void GoBack()
        {
            webview.GoBack();
        }

        public void GoForward()
        {
            webview.GoForward();
        }

        public void SetPosition(int x, int y)
        {
            webview.SetPosition(x, y);
        }

        public void SetSize(int width, int height)
        {
            webview.SetSize(width, height);
        }

        public void SetMargins(int left, int top, int right, int bottom)
        {
            webview.SetMargins(left, top, right, bottom);
        }

        public int GetX()
        {
            return webview.GetX();
        }

        public int GetY()
        {
            return webview.GetY();
        }

        public int GetWidth()
        {
            return webview.GetWidth();
        }

        public int GetHeight()
        {
            return webview.GetHeight();
        }

        public void ShowWebBrowser(string url)
        {
            webview.ShowWebBrowser(url);
        }
    }
}
