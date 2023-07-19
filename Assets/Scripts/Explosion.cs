using UnityEngine;
public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 2.6f);
    }
}