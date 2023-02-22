using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tween : MonoBehaviour
{
    [SerializeField] GameObject firstMenu, option_1_open, option_1_close, option_2_open, option_2_close, option_3_open, option_3_close, option_4_open, option_4_close;
    [SerializeField] Transform transformFirstMenu;


    public void showMenu(){
        LeanTween.scale(firstMenu, new Vector3(1.5f, 1.5f, 1.5f),1.2f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
    }

    // Controller Animations

    public void turnRight(){
        LeanTween.rotate(firstMenu, new Vector3(0, transformFirstMenu.eulerAngles.y+90f, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }

    public void turnLeft(){
        LeanTween.rotate(firstMenu, new Vector3(0, transformFirstMenu.eulerAngles.y-90f, 0),0.5f).setEase(LeanTweenType.easeInSine);
         // for external links: Application.OpenURL("http://unity3d.com/");
    
    }

    // Option Animations

    public void showOption_1(){
        LeanTween.scale(option_1_open, new Vector3(1, 1, 1),0.5f).setEase(LeanTweenType.easeInSine);
    }
    public void showOption_2(){
        LeanTween.scale(option_2_open, new Vector3(1, 1, 1),0.5f).setEase(LeanTweenType.easeInSine);
    }
    public void showOption_3(){
        LeanTween.scale(option_3_open, new Vector3(1, 1, 1),0.5f).setEase(LeanTweenType.easeInSine);
    }
    public void showOption_4(){
        LeanTween.scale(option_4_open, new Vector3(1, 1, 1),0.5f).setEase(LeanTweenType.easeInSine);
    }

    public void closeOption_1(){
        LeanTween.scale(option_1_close, new Vector3(1, 0, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }
    public void closeOption_2(){
        LeanTween.scale(option_2_close, new Vector3(1, 0, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }
    public void closeOption_3(){
        LeanTween.scale(option_3_close, new Vector3(1, 0, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }
    public void closeOption_4(){
        LeanTween.scale(option_4_close, new Vector3(1, 0, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }

}
