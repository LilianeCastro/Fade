using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireSpecial : MonoBehaviour
{ 
    [SerializeField] private Transform posSpawnSpecial;
    [SerializeField] private float     timeToUseSpecialAgain;
    [SerializeField] private float     energyToUseSpecial = 9.0f;

    private bool isSpecialUse;

    private void Update()
    {
        if(Input.GetButtonDown("Fire2") && !isSpecialUse && GameController.Instance.GetEnergyValue() > energyToUseSpecial)
        {
            isSpecialUse = true;

            GameController.Instance.UpdateEnergy(-10f);

            GameObject shotSpecial = Instantiate(GameController.Instance.shotSpecialPrefab, posSpawnSpecial.position, posSpawnSpecial.rotation);
            
            StartCoroutine("DelaySpecialToUseAgain");
        }
    }

    private IEnumerator DelaySpecialToUseAgain()
    {
        SoundManager.Instance.playFx(0);
        yield return new WaitForSeconds(timeToUseSpecialAgain);
        isSpecialUse = false;
    }
}
