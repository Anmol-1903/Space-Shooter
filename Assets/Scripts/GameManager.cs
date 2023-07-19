using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] bool isGameOver;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.Escape) && isGameOver)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void GameOver()
    {
        isGameOver = true;
    }
}