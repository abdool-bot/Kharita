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
                    if(data.Text.Equals("https://account.htw-berlin.de/ct/WHC579")){

                        string scannedRoomID = Regex.Match("https://lsf.htw-berlin.de/qisserver/rds?state=wplan&act=Raum&pool=Raum&show=plan&P.vx=kurz&raum.rgid=4582", pattern).Value;
                        string roomNumber = Regex.Match("https://account.htw-berlin.de/ct/WHC579", pattern).Value;
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

                    if(data.Text.Equals("https://account.htw-berlin.de/ct/WHC578")){

                        string scannedRoomID = Regex.Match("https://lsf.htw-berlin.de/qisserver/rds?state=wplan&act=Raum&pool=Raum&show=plan&P.vx=kurz&raum.rgid=4581", pattern).Value;
                        string roomNumber = Regex.Match("https://account.htw-berlin.de/ct/WHC578", pattern).Value;
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

                    if(data.Text.Equals("https://account.htw-berlin.de/ct/WHC577")){

                        string scannedRoomID = Regex.Match("https://lsf.htw-berlin.de/qisserver/rds?state=wplan&act=Raum&pool=Raum&show=plan&P.vx=kurz&raum.rgid=4580", pattern).Value;
                        string roomNumber = Regex.Match("https://account.htw-berlin.de/ct/WHC577", pattern).Value;
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
                else
                {
                    Debug.Log("No QR code detected !");
                }
            }
        }
    }
}    