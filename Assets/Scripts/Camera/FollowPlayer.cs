using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Player       _Player;
    public float        speedCamLerp;

    private Vector3     playerPosition;

    void FixedUpdate()
    {     
        //Follow player on X
        playerPosition = new Vector3(_Player.transform.position.x, transform.position.y, transform.position.z);

        //Follow player on Y
        transform.position = new Vector3(transform.position.x, _Player.transform.position.y + 1f, transform.position.z);
        
        transform.position = Vector3.Lerp(transform.position, playerPosition, speedCamLerp * Time.deltaTime);

        
    }
}
