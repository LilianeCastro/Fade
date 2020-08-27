using System.Collections;
using UnityEngine;

public class RaytracingEnemy : MonoBehaviour
{
    [Header("Ray")]
    public float distance;
    public float dirX;
    public float dirYUp;
    public float dirYDown;
    public float speedRay;
    private float dirY;
    private bool isRaycastCollider;
    

    void Start()
    {
        dirY = dirYDown;
        StartCoroutine("MoveToDown");
    }
    
    void Update()
    {
        //isRaycastCollider = Physics2D.Raycast(transform.position, Vector2.left, distance, 1 << LayerMask.NameToLayer("Player"));
        isRaycastCollider = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), new Vector2(dirX, dirY), distance, 1 << LayerMask.NameToLayer("Player"));

        if(isRaycastCollider)
        {
            print("------");
        }

        //Debug.DrawRay(transform.position, Vector2.left * distance, isRaycastCollider ? Color.yellow : Color.red);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.25f), new Vector2(dirX, dirY) * distance, isRaycastCollider ? Color.yellow : Color.red);
    }

    IEnumerator MoveToDown()
    {
        yield return new WaitForFixedUpdate();
        
        dirY -= speedRay * Time.deltaTime;

        if(dirY < dirYDown)
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

        if(dirY > dirYUp)
        {
            StartCoroutine("MoveToDown");
            StopCoroutine("MoveToUp");
        }

        StartCoroutine("MoveToUp");
    }

}
