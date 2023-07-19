using UnityEngine;
public class PowerUP : MonoBehaviour
{
    PlayerMovement PM;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _powerUpTime = 5f;
    
    // 0 => triple shot
    // 1 => speed
    // 2 => sheild
    [SerializeField] int _ID;
    [SerializeField] AudioSource audioSource;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
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
                switch (_ID)
                {
                    case 0:
                        PM.StartCoroutine(PM.PowerUpOff0(_powerUpTime));
                        break;
                    case 1:
                        PM.StartCoroutine(PM.PowerUpOff1(_powerUpTime));
                        break;
                    case 2:
                        PM.PowerUpOff2(_powerUpTime);
                        break;
                }
            }
            if (audioSource != null)
            {
                audioSource.Play();
            }
            sr.enabled = false;
            Destroy(this.gameObject, 1f);
        }
    }
}