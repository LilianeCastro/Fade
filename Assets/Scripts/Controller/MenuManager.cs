using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlatformType { Movement, Fall, Standing}
public class MenuManager : MonoSingleton<MenuManager>
{
    public void SceneToLoad(string nameSceneToLoad)
    {
        SceneManager.LoadSceneAsync(nameSceneToLoad);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
