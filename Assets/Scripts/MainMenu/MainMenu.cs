using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void LoadSinglePLayerGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMultiplayerPLayerGame()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}