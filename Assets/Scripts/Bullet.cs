using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 8f;
    private void Update()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
        if(transform.position.y > 8f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}