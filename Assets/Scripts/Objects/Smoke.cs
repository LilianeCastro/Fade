using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void OnEnable() {
        Destroy(this.gameObject, 0.25f);
    }
}
