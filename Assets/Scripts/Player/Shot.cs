using UnityEngine;

public class Shot : MonoBehaviour
{
    private void OnBecameInvisible() 
    {
        Destroy(this.gameObject);
    }
}
