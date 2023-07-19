using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    UIManager uIManager;

    [SerializeField] int _lives = 3;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _speedMultiplier = 2f;

    private float _currentSpeedMultiplier;

    [SerializeField] float _bulletCoolDown = 0.2f;
    [SerializeField] float _nextBulletTime;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _tripleshotPrefab;
    [SerializeField] GameObject _sheildVisualizer;


    [SerializeField] GameObject[] _thrusters;
    
    
    [SerializeField] Vector3 _bulletOffset;

    [SerializeField] bool isAlive = true;
    [SerializeField] bool trippleShotActive = false;
    [SerializeField] bool sheildActive = false;



    [SerializeField] int _score = 0;

    private void Start()
    {
        _currentSpeedMultiplier = 1f;
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
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

        transform.Translate(direction * _speed * _currentSpeedMultiplier * Time.deltaTime);

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
        if (sheildActive)
        {
            sheildActive = false;
            _sheildVisualizer.SetActive(false);
            return;
        }
        _lives--;
        uIManager.UpdateLives(_lives);
        if(_lives == 2)
        {
            _thrusters[Random.Range(0, _thrusters.Length)].SetActive(true);
        }
        if(_lives == 1)
        {
            _thrusters[0].SetActive(true);
            _thrusters[1].SetActive(true);
        }
        if(_lives < 1)
        {
            isAlive = false;
            Destroy(this.gameObject);
        }
    }
    public void AddScore(int points)
    {
        _score += points;
        uIManager.UpdateScore(_score);
    }
    public bool playerIsAlive()
    {
        return isAlive;
    }
    public IEnumerator PowerUpOff0(float _powerUpTime)
    {
        trippleShotActive = true;
        yield return new WaitForSeconds(_powerUpTime);
        trippleShotActive = false;
    }
    public IEnumerator PowerUpOff1(float _powerUpTime)
    {
        _currentSpeedMultiplier = _speedMultiplier;
        yield return new WaitForSeconds(_powerUpTime);
        _currentSpeedMultiplier = 1;
    }
    public IEnumerator PowerUpOff2(float _powerUpTime)
    {
        sheildActive = true;
        _sheildVisualizer.SetActive(true);
        yield return new WaitForSeconds(_powerUpTime);
        _sheildVisualizer.SetActive(false);
        sheildActive = false;
    }
}