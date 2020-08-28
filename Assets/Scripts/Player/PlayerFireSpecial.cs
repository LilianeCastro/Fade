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
        if(Input.GetButtonDown("Fire2") && !isSpecialUse)
        {
            isSpecialUse = true;

            GameController.Instance.UpdateEnergy(-10f);

            GameObject shotSpecial = Instantiate(GameController.Instance.shotSpecialPrefab, posSpawnSpecial.position, posSpawnSpecial.rotation);
            shotSpecial.TryGetComponent(out Rigidbody2D shotRb);

            shotRb.AddForce(Vector2.down * speedShot, ForceMode2D.Impulse);

            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * forcePlayerImpulse, ForceMode2D.Force);

            StartCoroutine("DelaySpecialToUseAgain");
        }
    }

    IEnumerator DelaySpecialToUseAgain()
    {
        yield return new WaitForSeconds(timeToUseSpecialAgain);
        isSpecialUse = false;
    }
}
