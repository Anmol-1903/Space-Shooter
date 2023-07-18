using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    PlayerMovement PM;
    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            float RandomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(RandomX, 8f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PM = other.GetComponent<PlayerMovement>();
            if(PM != null)
            {
                PM.TakeDamage();
            }
        }
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }
}