using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        IsOnCollosion(other, true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        IsOnCollosion(other, false);
    }

    void IsOnCollosion(Collision2D collisor, bool status)
    {
        if(collisor.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<PlatformMovement>().CollisionDetected(status);
        }
    }
}
