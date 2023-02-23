using System.Collections;
using System.Collections.Generic;
using Gpm.WebView;
using UnityEngine;
using UnityEngine.UI;

public class SampleWebView : MonoBehaviour
{
    private const string DEFAULT_URL = "https://www.google.com";

    public InputField urlInput;
    public InputField customSchemeInput;
    public InputField titleTextInput;
    public Toggle fullScreenToggle;
    public Toggle navigationBarVisibleToggle;
    public Toggle backButtonVisibleToggle;
    public Toggle forwardButtonVisibleToggle;
    public Dropdown orientationDropdown;
    public InputField popupXInput;
    public InputField popupYInput;
    public InputField popupWidthInput;
    public InputField popupHeightInput;
    public InputField popupMarginsInput;

    private void Awake()
    {
        urlInput.text = DEFAULT_URL;
    }

    public void OpenWebView()
    {
        if (string.IsNullOrEmpty(urlInput.text) == false)
        {
            GpmWebView.ShowUrl(urlInput.text, GetConfiguration(), OnWebViewCallback, GetCustomSchemeList());
        }
        else
        {
            Debug.LogError("[SampleWebView] Input url is empty.");
        }
    }

    public void OpenWithHTMLString()
    {
        string htmlString = @"<html><head><title>GPM WebView</title></head>
            <body><h1>GPM WebView Test</h1><h5>Hi.</h5>
            <script type='text/javascript'>
            function goGoogle() { window.open('https://www.google.com'); }
            </script>
            <button class='favorite styled' type='button' onclick='goGoogle()'>Go Google</button>
            </body></html>";
        GpmWebView.ShowHtmlString(htmlString, GetConfiguration(), OnWebViewCallback, GetCustomSchemeList());
    }

    public void OpenSafeBrowsing()
    {
        if (string.IsNullOrEmpty(urlInput.text) == false)
        {
            GpmWebViewSafeBrowsing.ShowSafeBrowsing(urlInput.text,
                new GpmWebViewRequest.ConfigurationSafeBrowsing
                {
                    navigationBarColor = "#4B96E6",
                    navigationTextColor = "#FFFFFF"
                }, OnWebViewCallback);
        }
        else
        {
            Debug.LogError("[SampleWebView] Input url is empty.");
        }
    }

    public void OpenWebBrowser()
    {
        if (string.IsNullOrEmpty(urlInput.text) == false)
        {
            GpmWebView.ShowWebBrowser(urlInput.text);
        }
        else
        {
            Debug.LogError("[SampleWebView] Input url is empty.");
        }
    }

    public void SetDefault()
    {
        fullScreenToggle.isOn = true;
        navigationBarVisibleToggle.isOn = true;
        backButtonVisibleToggle.isOn = true;
        forwardButtonVisibleToggle.isOn = true;
        orientationDropdown.value = 0;
        urlInput.text = DEFAULT_URL;
        customSchemeInput.text = string.Empty;
        titleTextInput.text = string.Empty;
        popupXInput.text = string.Empty;
        popupYInput.text = string.Empty;
        popupWidthInput.text = string.Empty;
        popupHeightInput.text = string.Empty;
        popupMarginsInput.text = string.Empty;
}

    private GpmWebViewRequest.Configuration GetConfiguration()
    {
        GpmWebViewRequest.CustomSchemePostCommand customSchemePostCommand = new GpmWebViewRequest.CustomSchemePostCommand();
        customSchemePostCommand.Close("CUSTOM_SCHEME_POST_CLOSE");

        return new GpmWebViewRequest.Configuration()
        {
            style = fullScreenToggle.isOn ? GpmWebViewStyle.FULLSCREEN : GpmWebViewStyle.POPUP,
            orientation = (orientationDropdown.value == 0) ? GpmOrientation.UNSPECIFIED : 1 << (orientationDropdown.value - 1),
            isClearCache = true,
            isClearCookie = true,
            title = string.IsNullOrEmpty(titleTextInput.text) ? string.Empty : titleTextInput.text,
            navigationBarColor = "#4B96E6",

            isNavigationBarVisible = navigationBarVisibleToggle.isOn,
            isBackButtonVisible = backButtonVisibleToggle.isOn,
            isForwardButtonVisible = forwardButtonVisibleToggle.isOn,
            supportMultipleWindows = true,

            customSchemePostCommand = customSchemePostCommand,

            position = GetConfigurationPosition(),
            size = GetConfigurationSize(),
            margins = GetConfigurationMargins(),

#if UNITY_IOS
            contentMode = GpmWebViewContentMode.RECOMMENDED,
            isMaskViewVisible = true,
            isAutoRotation = true
#endif
        };
    }

    private GpmWebViewRequest.Position GetConfigurationPosition()
    {
        bool hasValue = false;
        if (string.IsNullOrEmpty(popupXInput.text) == false && string.IsNullOrEmpty(popupYInput.text) == false)
        {
            hasValue = true;
        }

        int x = 0;
        int.TryParse(popupXInput.text, out x);

        int y = 0;
        int.TryParse(popupYInput.text, out y);

        return new GpmWebViewRequest.Position
        {
            hasValue = hasValue,
            x = x,
            y = y
        };
    }

    private GpmWebViewRequest.Size GetConfigurationSize()
    {
        bool hasValue = false;
        if (string.IsNullOrEmpty(popupWidthInput.text) == false && string.IsNullOrEmpty(popupHeightInput.text) == false)
        {
            hasValue = true;
        }

        int width = 0;
        int.TryParse(popupWidthInput.text, out width);

        int height = 0;
        int.TryParse(popupHeightInput.text, out height);

        return new GpmWebViewRequest.Size
        {
            hasValue = hasValue,
            width = width,
            height = height
        };
    }

    private GpmWebViewRequest.Margins GetConfigurationMargins()
    {
        bool hasValue = false;
        if (string.IsNullOrEmpty(popupMarginsInput.text) == false)
        {
            hasValue = true;
        }

        int margins = 0;
        int.TryParse(popupMarginsInput.text, out margins);

        return new GpmWebViewRequest.Margins
        {
            hasValue = hasValue,
            left = margins,
            top = margins,
            right = margins,
            bottom = margins
        };
    }

    private List<string> GetCustomSchemeList()
    {
        List<string> customSchemeList = null;
        if (string.IsNullOrEmpty(customSchemeInput.text) == false)
        {
            string[] schemes = customSchemeInput.text.Split(',');
            customSchemeList = new List<string>(schemes);
        }
        return customSchemeList;
    }

    private void OnWebViewCallback(GpmWebViewCallback.CallbackType callbackType, string data, GpmWebViewError error)
    {
        Debug.Log("OnWebViewCallback: " + callbackType);
        switch (callbackType)
        {
            case GpmWebViewCallback.CallbackType.Open:
                if (error != null)
                {
                    Debug.LogFormat("Fail to open WebView. Error:{0}", error);
                }
                break;
            case GpmWebViewCallback.CallbackType.Close:
                if (error != null)
                {
                    Debug.LogFormat("Fail to close WebView. Error:{0}", error);
                }
                break;
            case GpmWebViewCallback.CallbackType.PageLoad:
                if (string.IsNullOrEmpty(data) == false)
                {
                    Debug.LogFormat("Loaded Page:{0}", data);
                }
                break;
            case GpmWebViewCallback.CallbackType.MultiWindowOpen:
                Debug.Log("MultiWindowOpen");
                break;
            case GpmWebViewCallback.CallbackType.MultiWindowClose:
                Debug.Log("MultiWindowClose");
                break;
            case GpmWebViewCallback.CallbackType.Scheme:
                Debug.LogFormat("Scheme:{0}", data);
                break;
            case GpmWebViewCallback.CallbackType.GoBack:
                Debug.Log("GoBack");
                break;
            case GpmWebViewCallback.CallbackType.GoForward:
                Debug.Log("GoForward");
                break;
            case GpmWebViewCallback.CallbackType.ExecuteJavascript:
                Debug.LogFormat("ExecuteJavascript data : {0}, error : {1}", data, error);
                break;
        }
    }
}
