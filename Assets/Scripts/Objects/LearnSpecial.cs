using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnSpecial : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isPlayerOn", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isPlayerOn", false);
        }
    }
}
