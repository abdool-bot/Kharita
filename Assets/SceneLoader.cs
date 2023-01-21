using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneChanger(){
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void MenuLoader(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
