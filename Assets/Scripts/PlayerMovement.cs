using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool Player1;
    [SerializeField] bool Player2;
    UIManager uIManager;
    GameManager gameManager;

    [SerializeField] int _lives = 3;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _speedMultiplier;

    private float _currentSpeedMultiplier;

    [SerializeField] float _bulletCoolDown = 0.2f;
    [SerializeField] float _nextBulletTime;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _tripleshotPrefab;
    [SerializeField] GameObject _sheildVisualizer;

    [SerializeField] GameObject _explosionPrefab;
    

    [SerializeField] GameObject[] _thrusters;
    
    
    [SerializeField] Vector3 _bulletOffset;

    [SerializeField] bool isAlive = true;
    [SerializeField] bool trippleShotActive = false;
    [SerializeField] bool sheildActive = false;

    [SerializeField] int _score = 0;

    [SerializeField] AudioClip _bulletSound;
    [SerializeField] AudioSource _AudioSource;

    [SerializeField] Animator _animator;

    private void Start()
    {
        _currentSpeedMultiplier = 1f;
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _AudioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Player1)
        {
            Movement();
            if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && !gameManager.isPaused)) && Time.time > _nextBulletTime)
            {
                Shoot();
            }
        }
        else if (Player2)
        {
            Movement2();
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.RightControl)) && Time.time > _nextBulletTime)
            {
                Shoot2();
            }
        }
    }
    void Movement()
    {
        if (!isAlive)
        {
            return;
        }
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
        _animator.SetFloat("Dir",horizontal);
    }

    void Shoot()
    {
        _nextBulletTime = Time.time + _bulletCoolDown;
        if (trippleShotActive)
        {
            Instantiate(_tripleshotPrefab, this.transform.position + _bulletOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(_bullet, this.transform.position + _bulletOffset, Quaternion.identity);
        }
        if (_AudioSource != null)
        {
            _AudioSource.clip = _bulletSound;
            _AudioSource.Play();
        }
    }
    void Movement2()
    {
        if (!isAlive)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal2");
        float vertical = Input.GetAxis("Vertical2");

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
        _animator.SetFloat("Dir",horizontal);
    }

    void Shoot2()
    {
        _nextBulletTime = Time.time + _bulletCoolDown;
        if (trippleShotActive)
        {
            Instantiate(_tripleshotPrefab, this.transform.position + _bulletOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(_bullet, this.transform.position + _bulletOffset, Quaternion.identity);
        }
        if (_AudioSource != null)
        {
            _AudioSource.clip = _bulletSound;
            _AudioSource.Play();
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
        if (gameObject.CompareTag("Player"))
        {
            uIManager.UpdateLives(_lives);
        }
        else if (gameObject.CompareTag("Player2"))
        {
            uIManager.UpdateLives2(_lives);
        }
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
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 1f);
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
    public void PowerUpOff2(float _powerUpTime)
    {
        sheildActive = true;
        _sheildVisualizer.SetActive(true);
    }
}