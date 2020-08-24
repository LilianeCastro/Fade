using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform platform;
    public Transform[] posPlat;

    public float speed;
    private int idTarget;

    void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, posPlat[idTarget].position, speed * Time.deltaTime);

        if(platform.position == posPlat[idTarget].position)
        {
            idTarget += 1;
            if(idTarget == posPlat.Length)
            {
                idTarget = 0;
            }
        }
    }
}
