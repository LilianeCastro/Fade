using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Transform playerPos;
    public float vel = 0.1f;

    void Update()
    {
        transform.position = playerPos.position * vel;
    }
}
