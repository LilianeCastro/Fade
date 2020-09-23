using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireSpecial : MonoBehaviour
{ 
    public Transform        posSpawnSpecial;
    public float            forcePlayerImpulse;
    public float            speedShot;
    public float            timeToUseSpecialAgain;

    private Rigidbody2D     playerRb;
    private bool            isSpecialUse;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire2") && !isSpecialUse && GameController.Instance.GetEnergyValue() > 9f)
        {
            isSpecialUse = true;

            GameController.Instance.UpdateEnergy(-10f);

            GameObject shotSpecial = Instantiate(GameController.Instance.shotSpecialPrefab, posSpawnSpecial.position, posSpawnSpecial.rotation);
            
            StartCoroutine("DelaySpecialToUseAgain");
        }
    }

    IEnumerator DelaySpecialToUseAgain()
    {
        SoundManager.Instance.playFx(0);
        yield return new WaitForSeconds(timeToUseSpecialAgain);
        isSpecialUse = false;
    }
}
