using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tween : MonoBehaviour
{
    [SerializeField] GameObject 
        mainMenu, 
        option_1_open, 
        option_1_close, 
        option_2_open, 
        option_2_close, 
        option_3_open, 
        option_3_close, 
        option_4_open, 
        option_4_close;

    [SerializeField] Transform transformMainMenu;

    [SerializeField] RectTransform 
        option_1_hint_open, 
        option_1_hint_close, 
        option_2_hint_open, 
        option_2_hint_close, 
        option_3_hint_open, 
        option_3_hint_close,
        qr_code_hint;


    // Open Menu Animations

    public void showMenu(){
        LeanTween.scale(mainMenu, new Vector3(5f, 5f, 5f),1.6f).setEase(LeanTweenType.easeOutBack);
    }

    public void showMenu2(){
        LeanTween.scale(mainMenu, new Vector3(5f, 5f, 5f),1.6f).setEase(LeanTweenType.easeOutBack);
    }

    public void showMenu3(){
        LeanTween.scale(mainMenu, new Vector3(5f, 5f, 5f),1.6f).setEase(LeanTweenType.easeOutBack);
    }

    // Close Menu Animations

    public void closeMenu(){
        LeanTween.scale(mainMenu, new Vector3(0f, 0f, 0f),0.1f);
    }

    public void closeMenu2(){
        LeanTween.scale(mainMenu, new Vector3(0f, 0f, 0f),0.1f);
    }

    public void closeMenu3(){
        LeanTween.scale(mainMenu, new Vector3(0f, 0f, 0f),0.1f);
    }

    // Controller Animations

    public void turnRight(){
        LeanTween.rotate(mainMenu, new Vector3(0, transformMainMenu.eulerAngles.y+90f, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }

    public void turnLeft(){
        LeanTween.rotate(mainMenu, new Vector3(0, transformMainMenu.eulerAngles.y-90f, 0),0.5f).setEase(LeanTweenType.easeInSine);
    
    }

    // Open Option Animations

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

    // Close Option Animations

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

    // Open Option Hint Menu Animations

    public void openOption_1_Hint(){
        LeanTween.move(option_1_hint_open, new Vector3(0, 450, 0), 1.2f).setEase(LeanTweenType.easeOutBack);
    }

    public void openOption_2_Hint(){
        LeanTween.move(option_2_hint_open, new Vector3(0, 450, 0), 1.2f).setEase(LeanTweenType.easeOutBack);
    }

    public void openOption_3_Hint(){
        LeanTween.move(option_3_hint_open, new Vector3(0, 450, 0), 1.2f).setEase(LeanTweenType.easeOutBack);
    }

    // Close Option Hint Menu Animations

    public void closeOption_1_Hint(){
        LeanTween.move(option_1_hint_close, new Vector3(0, -450, 0), 0.1f);
    }

    public void closeOption_2_Hint(){
        LeanTween.move(option_2_hint_close, new Vector3(0, -450, 0), 0.1f);
    }

    public void closeOption_3_Hint(){
        LeanTween.move(option_3_hint_close, new Vector3(0, -450, 0), 0.1f);
    }

    // QR-Code Scan Animation

    public void qr_code_Hint(){
        LeanTween.move(qr_code_hint, new Vector3(0, 0, 0),1f).setDelay(0.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.move(qr_code_hint, new Vector3(0, -450, 0),1.6f).setDelay(6f).setEase(LeanTweenType.easeInBack);
    }

}
