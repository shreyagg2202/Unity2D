using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenePersist : MonoBehaviour
{
    int sceneIndex;
    private void Awake()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ScenePersist[] persists = FindObjectsOfType<ScenePersist>();
        foreach (var persist in persists)
        {
            if (persist != this)
            {
                if (persist.sceneIndex == currentSceneIndex)
                {
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    Destroy(persist.gameObject);
                }
            }
        }
        sceneIndex = currentSceneIndex;
        DontDestroyOnLoad(gameObject);
    }
}
