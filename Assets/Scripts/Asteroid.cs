using UnityEngine;
public class Asteroid : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    [SerializeField] GameObject _explosionPrefab;
    SpawnManager SM;
    private void Start()
    {
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if(SM != null)
            {
                SM.StartSpawning();
            }
            Destroy(this.gameObject,0.2f);
        }
    }
}