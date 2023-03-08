using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using ZXing;


public class QRCodeScanner : MonoBehaviour
{   
    private float timeInterval; 
    private bool cameraInitialized;

    [SerializeField]
    private QRCodeScanner scannerOn;

    [SerializeField]
    private Toggle scannerToggle;

    [SerializeField]
    private UI_Tween qrCodeAnimation;


    [SerializeField]
    private Text[] option_roomNumbers;

    private BarcodeReader barCodeReader;
    private string pattern = @"\d+(?!\d+)";
    public WebViewer webViewString = null;

    // ↓ Can be replaced with a database ↓

    private string baseURL_579 = "https://lsf.htw-berlin.de/qisserver/rds?state=wplan&act=Raum&pool=Raum&show=plan&P.vx=kurz&raum.rgid=4582"; 
    private string refURL_579 = "https://account.htw-berlin.de/ct/WHC579";

    private string baseURL_578 = "https://lsf.htw-berlin.de/qisserver/rds?state=wplan&act=Raum&pool=Raum&show=plan&P.vx=kurz&raum.rgid=4581"; 
    private string refURL_578 = "https://account.htw-berlin.de/ct/WHC578";

    private string baseURL_577 = "https://lsf.htw-berlin.de/qisserver/rds?state=wplan&act=Raum&pool=Raum&show=plan&P.vx=kurz&raum.rgid=4580"; 
    private string refURL_577 = "https://account.htw-berlin.de/ct/WHC577";


    void Start()
    {        
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);

        var isFrameFormatSet = VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(PixelFormat.RGB888, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }

    private void LateUpdate()
    {
        timeInterval += Time.deltaTime;
        if (timeInterval >= 0.5f){
            timeInterval = 0;
        
            if (cameraInitialized)
        {
                var cameraFeed = VuforiaBehaviour.Instance.CameraDevice.GetCameraImage(PixelFormat.RGB888);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.RGB24);
                if (data != null)
                {
                    if(data.Text.Equals(refURL_579)){

                        roomNumberSetup(baseURL_579, refURL_579, webViewString, option_roomNumbers, qrCodeAnimation, scannerOn, scannerToggle);
                    
                    }

                    if(data.Text.Equals(refURL_578)){

                        roomNumberSetup(baseURL_578, refURL_578, webViewString, option_roomNumbers, qrCodeAnimation, scannerOn, scannerToggle);

                    }

                    if(data.Text.Equals(refURL_577)){

                        roomNumberSetup(baseURL_577, refURL_577, webViewString, option_roomNumbers, qrCodeAnimation, scannerOn, scannerToggle);

                    }
                }
                else
                {
                    Debug.Log("No QR code detected !");
                }
            }
        }
    }

    private void roomNumberSetup(
        string baseURL,
        string refURL, 
        WebViewer webViewString, 
        Text[] option_roomNumbers, 
        UI_Tween qrCodeAnimation, 
        QRCodeScanner scannerOn, 
        Toggle scannerToggle)
        {
            string scannedRoomID = Regex.Match(baseURL, pattern).Value;
            string roomNumber = Regex.Match(refURL, pattern).Value;
            Debug.Log("Room ID is: " + scannedRoomID);
            webViewString.SetLSFRoomID(scannedRoomID);
            for(int i = 0; i < option_roomNumbers.Length; i++){
                option_roomNumbers[i].text = roomNumber;
            }

            qrCodeAnimation.qr_code_Hint();
            Handheld.Vibrate();

            scannerOn.enabled = false;
            scannerToggle.isOn = false;
            scannerToggle.Select();
    }
}    