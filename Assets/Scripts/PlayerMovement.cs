using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int _lives = 3;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _bulletCoolDown = 0.2f;
    [SerializeField] float _nextBulletTime;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _tripleshotPrefab;
    [SerializeField] Vector3 _bulletOffset;

    [SerializeField] bool isAlive = true;
    [SerializeField] bool trippleShotActive = false;
    
    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextBulletTime)
        {
            _nextBulletTime = Time.time + _bulletCoolDown;
            Shoot();
        }
    }
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0f);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0);

        if(transform.position.x > 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if(transform.position.x < -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        if (trippleShotActive)
        {
            Instantiate(_tripleshotPrefab, transform.position + _bulletOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(_bullet, transform.position + _bulletOffset, Quaternion.identity);
        }
    }

    public void TakeDamage()
    {
        _lives--;
        if(_lives < 1)
        {
            isAlive = false;
            Destroy(this.gameObject);
        }
    }
    public bool playerIsAlive()
    {
        return isAlive;
    }
     public IEnumerator PowerUpOff(float _powerUpTime)
    {
        trippleShotActive = true;
        yield return new WaitForSeconds(_powerUpTime);
        trippleShotActive = false;
    }
}