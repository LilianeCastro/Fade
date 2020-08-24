using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    public Transform    platform;
    public Transform    wheel;
    public float        speed;
    void Update()
    {
        wheel.Rotate(Vector3.forward * speed * Time.deltaTime);
        platform.Rotate(Vector3.forward * (speed * -1) * Time.deltaTime);
    }
}
