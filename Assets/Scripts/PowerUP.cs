using UnityEngine;
public class PowerUP : MonoBehaviour
{
    PlayerMovement PM;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _powerUpTime = 5f;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PM = collision.GetComponent<PlayerMovement>();
            if (PM != null)
            {
                PM.StartCoroutine(PM.PowerUpOff(_powerUpTime));
            }
            Destroy(this.gameObject);
        }
    }
}