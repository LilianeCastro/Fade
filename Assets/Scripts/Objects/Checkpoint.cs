using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator checkAnim;

    private void Start() {
        checkAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkAnim.SetTrigger("isPlayerOn");
            CheckpointController.Instance.SetPos(transform.position);
        }
    }
}
