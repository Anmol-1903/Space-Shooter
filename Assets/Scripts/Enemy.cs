using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    PlayerMovement PM1;
    PlayerMovement PM2;
    [SerializeField] private int points = 10;
    [SerializeField] Animator anim;


    [SerializeField] AudioSource _explosionSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        PM1 = GameObject.Find("Player (1)").GetComponent<PlayerMovement>();
        PM2 = GameObject.Find("Player (2)").GetComponent<PlayerMovement>();
        _explosionSound = GetComponent<AudioSource>();
    }
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
            if (PM1 != null)
            {
                PM1.TakeDamage();
            }
            if (anim != null)
            {
                anim.SetTrigger("OnEnemyDeath");
            }
            _speed = 0;
            _explosionSound.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
        }
        if (other.CompareTag("Player2"))
        {
            if (PM2 != null)
            {
                PM2.TakeDamage();
            }
            if (anim != null)
            {
                anim.SetTrigger("OnEnemyDeath");
            }
            _speed = 0;
            _explosionSound.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
        }
        if (other.CompareTag("Bullet"))
        {
            if (PM1 != null)
            {
                PM1.AddScore(points);
            }

            if (anim != null)
            {
                anim.SetTrigger("OnEnemyDeath");
            }
            _speed = 0;
            Destroy(other.gameObject);
            _explosionSound.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
        }
        if (other.CompareTag("Bullet"))
        {
            if (PM2 != null)
            {
                PM2.AddScore(points);
            }

            if (anim != null)
            {
                anim.SetTrigger("OnEnemyDeath");
            }
            _speed = 0;
            Destroy(other.gameObject);
            _explosionSound.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
        }
    }
}