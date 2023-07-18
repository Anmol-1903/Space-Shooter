using System.Collections;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] Transform _enemyContainer;
    [SerializeField] GameObject _powerUp;
    [SerializeField] float _enemySpawnInterval = 5f;
    [SerializeField] float[] _powerUpSpawnInterval;
    Vector3 randPos;
    PlayerMovement PM;
    private void Awake()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }
    IEnumerator SpawnEnemy()
    {
        while (PM.playerIsAlive())
        {
            randPos = new Vector3(Random.Range(-9f, 9f), 8f, 0f);
            GameObject newEnemy = Instantiate(_enemy, randPos, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer);
            yield return new WaitForSeconds(_enemySpawnInterval);
        }
    }
    IEnumerator SpawnPowerUp()
    {
        while (PM.playerIsAlive())
        {
            randPos = new Vector3(Random.Range(-9f, 9f), 8f, 0f);
            Instantiate(_powerUp, randPos, Quaternion.identity);
            yield return new WaitForSeconds(_powerUpSpawnInterval[Random.Range(0, _powerUpSpawnInterval.Length)]);
        }
    }
}