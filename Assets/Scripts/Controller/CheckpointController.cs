using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoSingleton<CheckpointController>
{
    private Vector3         respawnPos;
    public Transform        _Player;

    public void SetPos(Vector3 pos)
    {
        respawnPos = pos;
    }

    public void Restart()
    {
        _Player.position = respawnPos;
        
    }
}
