using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoSingleton<GameController>
{
    public CameraController _CameraController;
    public Player           _Player;

    public GameObject       shotPrefab;
    public GameObject       shotSpecialPrefab;
    public GameObject       smokePrefab;

    public Slider           energyProgress;
    public Image[]          lives;
    public Image[]          lifeSprite;

    public Image            keyHUD;
    public Image[]          keySprite;

    private int             life;
    private int             currentLife;
    private float           energyValue;

    void Start()
    {
        life = 3;
        currentLife = life;
        energyValue = energyProgress.maxValue;
    }

    public void UpdateKey(int value)
    {
        if(value == 1)
        {
            keyHUD.sprite = keySprite[1].sprite;
        }
        else
        {
            keyHUD.sprite = keySprite[0].sprite;
            MenuManager.Instance.SceneToLoad("WaterStage02");
        }
    }

    public void UpdateHp(int value)
    {
        
        currentLife += value;

        if (currentLife > 0)
        {
            if (value == -1)
            {
                lives[currentLife].sprite = lifeSprite[0].sprite;
                IsHit();
            }
            else
            {
                lives[currentLife].sprite = lifeSprite[1].sprite;
            }
        }
        else if (currentLife == 0)
        {
            IsHit();
            StartCoroutine("Respawn");
        }
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    private void ChangeScene(string scene)
    {
        MenuManager.Instance.SceneToLoad(scene);
    }

    public void FillHUD()
    {
        for(int i = 0; i < lives.Length; i++)
        {
            lives[i].sprite = lifeSprite[1].sprite;
        }

        energyProgress.value = energyProgress.maxValue;
        currentLife = life;
        
    }

    public void UpdateEnergy(float value)
    {
        energyProgress.value += value;
        print(energyProgress.value);
    }

    public float GetEnergyValue()
    {
        return energyValue;
    }


    private void IsHit()
    {
        StartCoroutine(_CameraController.Shake(.5f, .3f));
    }

    IEnumerator Respawn()
    {
        CanvasController.Instance.FadeOut();

        yield return new WaitForSeconds(0.5f);

        FillHUD();
        CheckpointController.Instance.Restart();
        
    }
}
