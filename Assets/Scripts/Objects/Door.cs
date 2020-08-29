using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnim;

    private void Start() {
        doorAnim = GetComponent<Animator>();    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.TryGetComponent(out Player _Player);
            print(_Player.IsPlayerHasKey());
            if(_Player.IsPlayerHasKey())
            {
                doorAnim.SetTrigger("hasKey");
            }
        }
    }
}
