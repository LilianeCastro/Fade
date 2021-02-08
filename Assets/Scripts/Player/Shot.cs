using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
    }
    
    private void OnBecameInvisible() 
    {
        Destroy(this.gameObject);
    }
}
