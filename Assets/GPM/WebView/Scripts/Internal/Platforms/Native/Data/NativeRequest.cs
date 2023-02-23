namespace Gpm.WebView.Internal
{
    using System.Collections.Generic;

    public static class NativeRequest
    {
        public class Configuration
        {
            public int style;
            public int orientation;
            public bool isClearCookie;
            public bool isClearCache;
            public bool isNavigationBarVisible;
            public string navigationBarColor;
            public string title;
            public bool isBackButtonVisible;
            public bool isForwardButtonVisible;
            public bool supportMultipleWindows;
            public string userAgentString;
            public string addJavascript;

            /// <summary>
            /// Only used in style of Popup.
            /// </summary>
            public bool hasPosition;
            public int positionX;
            public int positionY;
            public bool hasSize;
            public int sizeWidth;
            public int sizeHeight;
            public bool hasMargins;
            public int marginsLeft;
            public int marginsTop;
            public int marginsRight;
            public int marginsBottom;

            /// <summary>
            /// iOS only
            /// </summary>
            public int contentMode;
            public bool isMaskViewVisible;
            public bool isAutoRotation;

            public List<string> schemeCommandList;
        }

        public class ConfigurationSafeBrowsing
        {
            public string navigationBarColor = "#4B96E6";

            // iOS only.
            public string navigationTextColor = "#FFFFFF";
        }

        [System.Obsolete("This class is deprected.")]
        public class ShowWebViewDeprecated
        {
            public string data;
            public Configuration configuration;
            public int callback = -1;
            public int closeCallback = -1;
            public List<string> schemeList;
            public int schemeEvent = -1;
            public int pageLoadCallback = -1;
        }

        public class ShowWebView
        {
            public string data;
            public Configuration configuration;
            public List<string> schemeList;
        }

        public class ShowSafeBrowsing
        {
            public string url;
            public ConfigurationSafeBrowsing configuration;
        }

        public class ExecuteJavaScript
        {
            public string script;
        }

        /// <summary>
        /// Position of popup style webView
        /// </summary>
        public class Position
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Size of popup style webView
        /// </summary>
        public class Size
        {
            public int width;
            public int height;
        }

        /// <summary>
        /// Margins of popup style webView
        /// </summary>
        public class Margins
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public class ShowWebBrowser
        {
            public string url;
        }
    }
}
