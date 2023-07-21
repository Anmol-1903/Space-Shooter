using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] public bool isPaused;
    [SerializeField] bool isGameOver;
    [SerializeField] Animator animator;
    private void Start()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isGameOver)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void GameOver()
    {
        isGameOver = true;
    }
    public void Pause()
    {
        isPaused = true;
        animator.SetBool("isPaused", isPaused);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isPaused = false;
        animator.SetBool("isPaused", isPaused);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Volume(float vol)
    {
        if (isPaused)
        {
            mixer.SetFloat("Master", vol);
        }
    }
}