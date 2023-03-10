using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gpm.WebView;

public class WebViewer : MonoBehaviour  {

    // Public variables to either set or retrieve information or URL's for the Website methods
    public string completeUrl = "";
    public string currentRoomID = "";

    // Method to open HTW related websites
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

    // Method to open LSF with the desired / scanned room number at the end
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
