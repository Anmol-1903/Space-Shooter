using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIManager : MonoBehaviour
{
    GameManager GM;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highScoreText;
    [SerializeField] TextMeshProUGUI _restartText;
    [SerializeField] TextMeshProUGUI _mainMenuText;
    [SerializeField] Image _livesDisplay;
    [SerializeField] Image _livesDisplay2;
    [SerializeField] GameObject _gameOverText;
    int high;
    [SerializeField] Sprite[] _liveSprites;
    private void Start()
    {
        high = PlayerPrefs.GetInt("HIGHSCORE");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOverText.SetActive(false);
        _scoreText.text = "Score : " + 0;
        _highScoreText.text = "Highscore : " + high.ToString();
    }
    public void UpdateScore(int newScore)
    {
        _scoreText.text = "Score : " + newScore.ToString();
        if(newScore > high)
        {
            high = newScore;
            PlayerPrefs.SetInt("HIGHSCORE", high);
        }
    }
    public void UpdateLives(int currentLive)
    {
        _livesDisplay.sprite = _liveSprites[currentLive];
        if (currentLive == 0)
        {
            if (GM != null)
            {
                GM.GameOver();
            }
            StartCoroutine(Flicker());
        }
    }
    public void UpdateLives2(int currentLive)
    {
        _livesDisplay2.sprite = _liveSprites[currentLive];
        if (currentLive == 0)
        {
            if (GM != null)
            {
                GM.GameOver();
            }
            StartCoroutine(Flicker());
        }
    }
    IEnumerator Flicker()
    {
        yield return new WaitForSeconds(1f);
        _gameOverText.SetActive(true);
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