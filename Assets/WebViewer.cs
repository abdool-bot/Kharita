using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gpm.WebView;

public class WebViewer : MonoBehaviour  {

    public string completeUrl = "";
    public string currentRoomID = "";

    public void OpenHTWWebsitesFULL()
    {
        GpmWebView.ShowUrl(
            completeUrl,
            new GpmWebViewRequest.Configuration()
            {
                style = GpmWebViewStyle.FULLSCREEN,
                orientation = GpmOrientation.PORTRAIT,
                isClearCookie = true,
                isClearCache = true,
                isNavigationBarVisible = true,
                navigationBarColor = "#8ED936",
                title = "Kharita - Browser", 
                isBackButtonVisible = true,
                isForwardButtonVisible = true,
                supportMultipleWindows = true,
    #if UNITY_IOS
                contentMode = GpmWebViewContentMode.MOBILE,
                isMaskViewVisible = true,
    #endif
            }, null, null);
    }

    public void OpenLSFScheduleFULL()
    {
        GpmWebView.ShowUrl(
            completeUrl + currentRoomID,
            new GpmWebViewRequest.Configuration()
            {
                style = GpmWebViewStyle.FULLSCREEN,
                orientation = GpmOrientation.PORTRAIT,
                isClearCookie = true,
                isClearCache = true,
                isNavigationBarVisible = true,
                navigationBarColor = "#8ED936",
                title = "Kharita - Browser", 
                isBackButtonVisible = true,
                isForwardButtonVisible = true,
                supportMultipleWindows = true,
    #if UNITY_IOS
                contentMode = GpmWebViewContentMode.MOBILE,
                isMaskViewVisible = true,
    #endif
            }, null, null);
    }

public void SetWholeURL(string newURL){
    completeUrl = newURL;
}

public void SetLSFRoomID(string retrievedRoomID){
    currentRoomID = retrievedRoomID;
}

}
