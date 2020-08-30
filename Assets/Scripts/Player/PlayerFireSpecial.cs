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

    private void Update()
    {
        if(Input.GetButtonDown("Fire2") && !isSpecialUse && GameController.Instance.GetEnergyValue() > 9f)
        {
            isSpecialUse = true;

            GameController.Instance.UpdateEnergy(-10f);

            GameObject shotSpecial = Instantiate(GameController.Instance.shotSpecialPrefab, posSpawnSpecial.position, posSpawnSpecial.rotation);
            shotSpecial.TryGetComponent(out Rigidbody2D shotRb);

            shotRb.AddForce(Vector2.down * speedShot, ForceMode2D.Impulse);

            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(Vector2.up * forcePlayerImpulse);

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
