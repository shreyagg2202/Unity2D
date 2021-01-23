using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader 
{ 
    public static void Loading()
    {
        SceneManager.LoadScene("Loading");
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene("GameScene");
    }

}
