using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerFire : MonoSingleton<GameControllerFire>
{
    public GameObject       shotPrefab;
    public GameObject       shotSpecialPrefab;
    public GameObject       smokePrefab;

    private void ChangeScene(string scene)
    {
        MenuManager.Instance.SceneToLoad(scene);
    }

    public void Death()
    {
        ChangeScene(MenuManager.Instance.GetSceneName());
        
    }

}
