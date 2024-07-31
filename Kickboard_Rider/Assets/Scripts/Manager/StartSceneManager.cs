using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Main");
        //SceneManager.LoadScene("LoadingScene");
    }
}

