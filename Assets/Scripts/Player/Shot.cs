using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(this.gameObject, 5f);
    }

}
