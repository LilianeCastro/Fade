using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player                   _Player;
    public float                    speedCamLerp;

    private Vector3                 playerPosition;

    void Start()
    {
        StartCoroutine("Follow");
    }

    public IEnumerator Follow()
    {
        yield return new WaitForFixedUpdate();

        //Follow player on X
        playerPosition = new Vector3(_Player.transform.position.x, transform.position.y, transform.position.z);

        //Follow player on Y
        transform.position = new Vector3(transform.position.x, _Player.transform.position.y + 1.5f, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, playerPosition, speedCamLerp * Time.deltaTime);

        StartCoroutine("Follow");
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        StopCoroutine("Follow");

        yield return new WaitForEndOfFrame();

        Vector3 originPos = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, originPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

        StartCoroutine("Follow");
    }
}
