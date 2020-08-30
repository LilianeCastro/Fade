using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    [Header("Player GameObject")]
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSr;
    private Animator playerAnim;
    public Transform posSpawn;
    public Transform groundCheckL;
    public Transform groundCheckR;
    public LayerMask layerCheck;

    [Header("Player Config")]
    public TypePlayer typePlayer;
    private bool isGrounded;
    private float horizontal;
    public bool isLookLeft;
    public float speedX;
    public float forceJump;
    public float timeInvencible;
    private bool isInvencible;

    [Header("Shot Config")]
    public float shotSpeed;
    public float delayShot;
    private bool cantShot;
    private int directionShot;
    private GameObject shotTemp;

    private bool isGetKey;

    private void Start()
    {
        directionShot = 1;
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckL.position, 0.02f, layerCheck) ||
                     Physics2D.OverlapCircle(groundCheckR.position, 0.02f, layerCheck);
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        playerRb.velocity = new Vector2(horizontal * speedX, playerRb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetButtonUp("Fire1") && !cantShot)
        {
            StartCoroutine("Fire");
        }

        if (horizontal > 0 && !isLookLeft)
        {
            Flip();
            directionShot = 1;
        }
        else if (horizontal < 0 && isLookLeft)
        {
            Flip();
            directionShot = -1;
        }
        if (horizontal != 0)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }

        playerAnim.SetBool("isGrounded", isGrounded);
        print(playerRb.velocity.y);
        playerAnim.SetFloat("speedY", playerRb.velocity.y);
    }

    private void Jump()
    {
        SoundManager.Instance.playFx(2);
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
        playerRb.AddForce(Vector2.up * forceJump);
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public bool IsInvencible()
    {
        return isInvencible;
    }

    public void CurrentHp(int value)
    {
        if (value == -1 && !isInvencible)
        {
            GameController.Instance.UpdateHp(value);

            if (GameController.Instance.GetCurrentLife() > 0)
            {
                isInvencible = true;
                StartCoroutine("Invencible", timeInvencible);

                playerRb.velocity = Vector2.zero;
                playerRb.AddForce(Vector2.up * 100);
            }

        }
        else if (value == 0)
        {
            GameController.Instance.UpdateHp(value);
        }

    }

    public bool IsPlayerHasKey()
    {
        return isGetKey;
    }

    IEnumerator Invencible(float time)
    {
        playerSr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        playerSr.color = Color.clear;
        yield return new WaitForSeconds(0.1f);

        if (time < 0)
        {
            isInvencible = false;
            playerSr.color = Color.white;

            StopCoroutine("Invencible");
            yield return null;
        }

        StartCoroutine("Invencible", time - Time.deltaTime);
    }

    IEnumerator Fire()
    {
        cantShot = true;

        if (GameController.Instance.GetEnergyValue() >= 4)
        {
            playerAnim.SetLayerWeight(1, 1);
            GameController.Instance.UpdateEnergy(-5f);

            SoundManager.Instance.playFx(1);

            shotTemp = Instantiate(GameController.Instance.shotPrefab, posSpawn.position, posSpawn.rotation);
            shotTemp.TryGetComponent(out Rigidbody2D shotRb);

            shotRb.AddForce(Vector2.right * shotSpeed * directionShot, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(delayShot);

        playerAnim.SetLayerWeight(1, 0);
        playerAnim.SetLayerWeight(0, 1);

        cantShot = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "PlatformMovement":
                transform.parent = other.transform;
                break;

            case "ObjectDamage":
                CurrentHp(-1);
                StartCoroutine("IsInDamage");
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
            case "ObjectDamage":
                StopCoroutine("IsInDamage");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                CurrentHp(-1);
                break;
            case "Key":
                GameController.Instance.UpdateKey(1);
                isGetKey = true;
                Destroy(other.gameObject);
                break;
            case "Heart":
                GameController.Instance.UpdateHp(1);
                break;
            case "ChangeScene":
                if (isGetKey)
                {
                    GameController.Instance.UpdateKey(0);
                }
                break;
        }
    }

    IEnumerator IsInDamage()
    {
        yield return new WaitForSeconds(1f);
        CurrentHp(-1);
        StartCoroutine("IsInDamage");
    }
}
