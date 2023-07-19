using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIManager : MonoBehaviour
{
    GameManager GM;

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _restartText;
    [SerializeField] TextMeshProUGUI _mainMenuText;
    [SerializeField] Image _livesDisplay;
    [SerializeField] GameObject _gameOverText;

    [SerializeField] Sprite[] _liveSprites;
    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOverText.SetActive(false);
        _scoreText.text = "Score : " + 0;
    }
    public void UpdateScore(int newScore)
    {
        _scoreText.text = "Score : " + newScore.ToString();
    }
    public void UpdateLives(int currentLive)
    {
        _livesDisplay.sprite = _liveSprites[currentLive];
        if(currentLive == 0)
        {
            if (GM != null)
            {
                GM.GameOver();
            }
            _gameOverText.SetActive(true);
            StartCoroutine(Flicker());
        }
    }
    IEnumerator Flicker()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            _restartText.text = "Press 'R' to Restart";
            _mainMenuText.text = "'Esc' for Main Menu";
            yield return new WaitForSeconds(0.5f);
            _restartText.text = "";
            _mainMenuText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}