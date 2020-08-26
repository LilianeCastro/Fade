using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float        speed;
    public float        flipTime;

    void Start()
    {
        speed *= -1;
        InvokeRepeating("Flip", flipTime, flipTime);
    }

    private void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    void Flip()
    {
        speed *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag)
        {
            case "PlatformMovement":
                transform.parent = transform.parent = other.transform;
                break;

            case "Shot":
                Instantiate(GameController.Instance.smokePrefab, this.transform.position, this.transform.rotation);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }
}
