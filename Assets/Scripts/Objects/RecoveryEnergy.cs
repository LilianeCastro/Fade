using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryEnergy : MonoBehaviour
{
    public TypePlayer typePlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {  
        if(other.gameObject.CompareTag("Player"))
        {
            CheckValuesToUpdateFill(other, true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CheckValuesToUpdateFill(other, false);
        }
    }

    IEnumerator FillEnergy()
    {
        yield return new WaitForEndOfFrame();

        GameController.Instance.UpdateEnergy(0.1f);

        StartCoroutine("FillEnergy");
    }

    private void CheckValuesToUpdateFill(Collider2D other, bool canFill)
    {
        other.gameObject.TryGetComponent(out Player _Player);
        if(_Player.typePlayer.Equals(TypePlayer.Fire) && canFill)
        {
            StartCoroutine("FillEnergy");
        }
        else if(_Player.typePlayer.Equals(TypePlayer.Fire) && !canFill)
        {
            StopCoroutine("FillEnergy");
        }
    }
}
