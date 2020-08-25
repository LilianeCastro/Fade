using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private int idTarget;

    public Platform         _Platform;
    public Transform[]      posPlat;

    public PlatformType     platformType;
    public float            speed;
    
    void Start()
    {
        if(platformType.Equals(PlatformType.Movement))
        {
            StartCoroutine("PlatformMove");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            print("Player");
        }
    }

    IEnumerator PlatformMove()
    {
        yield return new WaitForEndOfFrame();
        _Platform.transform.position = Vector3.MoveTowards(_Platform.transform.position, posPlat[idTarget].position, speed * Time.deltaTime);

        if (_Platform.transform.position.Equals(posPlat[idTarget].position))
        {
            idTarget += 1;
            if (idTarget == posPlat.Length)
            {
                idTarget = 0;
            }
        }
        StartCoroutine("PlatformMove");
    }

    IEnumerator PlatformFall()
    {
        yield return new WaitForEndOfFrame();
        _Platform.TryGetComponent(out Rigidbody2D _PlatformRb);
        _PlatformRb.bodyType = RigidbodyType2D.Dynamic;

    }

    IEnumerator PlatformFallReturnToOrigin()
    {
        _Platform.TryGetComponent(out Rigidbody2D _PlatformRb);
        _PlatformRb.bodyType = RigidbodyType2D.Static;

        yield return new WaitForEndOfFrame();
        StartCoroutine("PlatformReturnMove");
        
    }

    IEnumerator PlatformReturnMove()
    {
        yield return new WaitForEndOfFrame();

        _Platform.transform.position = Vector3.MoveTowards(_Platform.transform.position, posPlat[0].position, speed * Time.deltaTime);

        if(_Platform.transform.position.Equals(posPlat[0].position))
        {
            StopCoroutine("PlatformReturnMove");
        }
        else
        {
            StartCoroutine("PlatformReturnMove");
        }

    }

    public void CollisionDetected(bool isOnCollision)
    {
        if(isOnCollision)
        {
            StartCoroutine("PlatformFall");
        }else
        {
            print("saiu");
            StopCoroutine("PlatformFall");
            StartCoroutine("PlatformFallReturnToOrigin");
        }
        
    }
}
