using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //Para o raycast aparecer, tem que colocar o Debug.drawray no update, que so aparece se tiver no update
public class EnemyRaytracing : MonoBehaviour
{
    [Header("Ray")]
    public float            distance;
    public float            dirX;
    public float            dirYUp;
    public float            dirYDown;
    public float            speedRay;
    public Vector2          OriginPos;

    private float           dirY;
    private bool            isRaycastCollider;
    
    [Header("Lights")]
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    public GameObject       LightTransform;
    public float            speedLight;
    public float            dirZUp;
    public float            dirZDown;

    private float           dirZ;
    private Vector2         origin;
    
    private bool            isColliding;

    private Player          _Player;

    void OnEnable()
    {
        if(GameObject.Find("PlayerFire")==null)
        {
            _Player = GameObject.Find("PlayerWater").GetComponent<Player>();
        }else
        {
            _Player = GameObject.Find("PlayerFire").GetComponent<Player>();
        }
        
        //Light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        dirY = dirYUp;
        dirZ = dirZUp;

        StartCoroutine("MoveToUp");
        
    }
    
    void Update()
    {
        origin = new Vector2(transform.position.x + OriginPos.x, transform.position.y + OriginPos.y);

        isRaycastCollider = Physics2D.Raycast(origin, new Vector3(dirX, dirY), distance, 1 << LayerMask.NameToLayer("Player")) ||
        Physics2D.Raycast(origin, new Vector3(dirX, dirY - 0.2f), distance, 1 << LayerMask.NameToLayer("Player"));

        if(isRaycastCollider && !_Player.IsInvencible())
        {
            _Player.CurrentHp(-1);
        }
        

        Debug.DrawRay(origin, new Vector3(dirX, dirY) * distance, isRaycastCollider ? Color.yellow : Color.red);
        Debug.DrawRay(origin, new Vector3(dirX, dirY - 0.2f) * distance, isRaycastCollider ? Color.yellow : Color.red);

    }

    IEnumerator MoveToDown()
    {
        yield return new WaitForFixedUpdate();
        
        dirY -= speedRay * Time.deltaTime;

        if(dirX < 0)
        {
            dirZ += speedLight * Time.deltaTime;
        }
        else{
            dirZ -= speedLight * Time.deltaTime;
        }
        
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

        if(dirX < 0)
        {
            dirZ -= speedLight * Time.deltaTime;
        }
        else{
            dirZ += speedLight * Time.deltaTime;
        }
        LightTransform.transform.localRotation = Quaternion.Euler(0, 0, dirZ);

        if(dirY >= dirYUp)
        {
            StartCoroutine("MoveToDown");
            StopCoroutine("MoveToUp");
        }

        StartCoroutine("MoveToUp");
    }

}
