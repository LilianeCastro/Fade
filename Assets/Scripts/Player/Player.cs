using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player GameObject")]
    private Rigidbody2D     playerRb;
    private SpriteRenderer  playerSr;
    public Transform        posSpawn;
    public Transform        groundCheck;
    public LayerMask        layerCheck;

    [Header("Player Config")]
    private bool            isGrounded;
    private float           horizontal;
    public bool             isLookLeft;
    public float            speedX;
    public float            forceJump;
    public float            timeInvencible;
    private bool            isInvencible;

    [Header("Shot Config")]
    public float            shotSpeed;
    public float            delayShot;
    private bool            cantShot;
    private int             directionShot;
    private GameObject      shotTemp;

    private void Start()
    {
        directionShot = 1;
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, layerCheck);
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        playerRb.velocity = new Vector2(horizontal * speedX, playerRb.velocity.y);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if(Input.GetButtonUp("Fire1") && !cantShot)
        {
            StartCoroutine("Fire");
            Fire();
        }

        if(horizontal > 0 && isLookLeft)
        {
            Flip();
            directionShot = 1;
        }
        else if(horizontal < 0 && !isLookLeft)
        {
            Flip();
            directionShot = -1;
        }
        
    }

    private void Jump()
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(Vector2.up * forceJump);
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator Fire()
    {
        cantShot = true;

        shotTemp = Instantiate(GameController.Instance.shotPrefab, posSpawn.position, posSpawn.rotation);
        shotTemp.TryGetComponent(out Rigidbody2D shotRb);
       
        shotRb.AddForce(Vector2.right * shotSpeed * directionShot, ForceMode2D.Impulse);

        yield return new WaitForSeconds(delayShot);

        cantShot = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "PlatformMovement":

                transform.parent = other.transform;
                break;

            case "ObjectDamage":
                UpdateHp(-1);
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "PlatformMovement":
                transform.parent = null;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                UpdateHp(-1);
                break;
        }
    }

    void UpdateHp(int value)
    {
        if (value == -1 && !isInvencible)
        {
            GameController.Instance.UpdateHp(value);

            if(GameController.Instance.GetCurrentLife() > 0)
            {
                isInvencible = true;
                StartCoroutine("Invencible", timeInvencible);

                playerRb.velocity = Vector2.zero;
                playerRb.AddForce(Vector2.up * 200);
            }
            
        }
        else if(value == 0)
        {
            GameController.Instance.UpdateHp(value);
        }
        
    }

    IEnumerator Invencible(float time)
    {
        playerSr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        playerSr.color = Color.clear;
        yield return new WaitForSeconds(0.1f);

        if(time < 0)
        {
            print("entrou");
            isInvencible = false;
            playerSr.color = Color.white;

            StopCoroutine("Invencible");
            yield return null;
        }

        StartCoroutine("Invencible", time - Time.deltaTime);
    }
}
