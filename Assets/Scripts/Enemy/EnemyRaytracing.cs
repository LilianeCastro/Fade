using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaytracing : MonoBehaviour
{
    [Header("Ray")]
    public float distance;
    public float dirX;
    public float dirYUp;
    public float dirYDown;
    public float speedRay;

    private float dirY;
    private bool isRaycastCollider;
    
    [Header("Lights")]
    //public GameObject Light;
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    public GameObject LightTransform;
    public float speedLight;
    public float dirZUp;
    public float dirZDown;

    private float dirZ;

    public float dirZs;

    void OnEnable()
    {
        Light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        dirY = dirYUp;
        dirZ = dirZUp;

        StartCoroutine("MoveToUp");
    }
    
    void Update()
    {
        //isRaycastCollider = Physics2D.Raycast(transform.position, Vector2.left, distance, 1 << LayerMask.NameToLayer("Player"));
        isRaycastCollider = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), new Vector3(dirX, dirY, dirZs), distance, 1 << LayerMask.NameToLayer("Player"));

        if(isRaycastCollider)
        {
            print("------");
        }

        //Debug.DrawRay(transform.position, Vector2.left * distance, isRaycastCollider ? Color.yellow : Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.25f), new Vector3(dirX, dirY, dirZs) * distance, isRaycastCollider ? Color.yellow : Color.red);
    }

    IEnumerator MoveToDown()
    {
        yield return new WaitForFixedUpdate();
        
        dirY -= speedRay * Time.deltaTime;

        dirZ += speedLight * Time.deltaTime;
        LightTransform.transform.localRotation = Quaternion.Euler(0, 0, dirZ);

        if(dirY <= dirYDown)
        {
            StartCoroutine("MoveToUp");
            StopCoroutine("MoveToDown");
        }

        StartCoroutine("MoveToDown");
    }

    IEnumerator MoveToUp()
    {
        yield return new WaitForFixedUpdate();
        
        dirY += speedRay * Time.deltaTime;

        dirZ -= speedLight * Time.deltaTime;
        LightTransform.transform.localRotation = Quaternion.Euler(0, 0, dirZ);

        if(dirY >= dirYUp)
        {
            StartCoroutine("MoveToDown");
            StopCoroutine("MoveToUp");
        }

        StartCoroutine("MoveToUp");
    }

}
