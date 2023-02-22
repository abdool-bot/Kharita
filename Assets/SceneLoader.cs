using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneChangeToKharita(){
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void SceneChangeToKharitaAdmin(){
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void MenuLoader(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
