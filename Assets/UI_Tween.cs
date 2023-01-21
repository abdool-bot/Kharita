using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tween : MonoBehaviour
{
    [SerializeField] GameObject firstMenu, doorMenu, doorSubMenu;
    [SerializeField] Transform transformFirstmMenu;


    public void showMenu(){
        LeanTween.scale(firstMenu, new Vector3(1, 1, 1),1.2f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
    }

    public void turnRight(){
        LeanTween.rotate(firstMenu, new Vector3(0, transformFirstmMenu.eulerAngles.y+90f, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }

    public void turnLeft(){
        LeanTween.rotate(firstMenu, new Vector3(0, transformFirstmMenu.eulerAngles.y-90f, 0),0.5f).setEase(LeanTweenType.easeInSine);
    }

    public void showDoorMenu(){
        LeanTween.scale(doorMenu, new Vector3(1, 1, 1),0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
        //LeanTween.move(doorMenu, new Vector3(5.75f, 0, 0),1.2f).setDelay(1f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(doorSubMenu, new Vector3(1, 1, 1),0.5f).setDelay(1.2f).setEase(LeanTweenType.easeOutBack);
        //LeanTween.move(doorSubMenu, new Vector3(4.65f, -3f, 0),1.2f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
    }

    private void Update() {
        //Debug.Log(transformFirstmMenu.rotation.y);
    }
}
