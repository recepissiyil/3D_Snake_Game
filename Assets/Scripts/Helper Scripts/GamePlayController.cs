using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// This class is attached GamePlayController gameobject at scene 1 to 10
public class GamePlayController : MonoBehaviour
{
    #region Variables
    public static GamePlayController instance;
    private float minX = -2f, maxX = 2.6f, minY = -4.7f, maxY = 3.95f;
    private float zPos = 5.8f;

    public AudioClip buttonSound;

    [Header("PickUps")]
    public GameObject fruitPickup, bombPickup;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject failedPanel;
    public GameObject successPanel;

    [Header("Counter")]
    public float counter;
    public TextMeshProUGUI counterText;

    [Header("Score")]
    public int scoreCount;
    public int highScore1;
    public int highScore2;
    public int highScore3;
    public TextMeshProUGUI scoreText;

    [Header("Failed")]
    public TextMeshProUGUI highScoreText1;
    public TextMeshProUGUI highScoreText2;
    public TextMeshProUGUI highScoreText3;
    public TextMeshProUGUI yourScoreText;

    [Header("Successed")]
    public TextMeshProUGUI highScoreText1S;
    public TextMeshProUGUI highScoreText2S;
    public TextMeshProUGUI highScoreText3S;
    public TextMeshProUGUI yourScoreTextS;

    [Header("Sound")]
    public bool isSoundActive;
    public List<Sprite> soundSprites;
    public Image soundImage;

    [Header("Effect")]
    public GameObject eatingEffect;
    public GameObject bombEffect;
    #endregion

    #region Definitions before the scene opens
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isSoundActive = true;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Invoke("StartSpawning", .5f);
        highScore1 = PlayerPrefs.GetInt("Player 1");
        highScore2 = PlayerPrefs.GetInt("Player 2");
        highScore3 = PlayerPrefs.GetInt("Player 3");
        StartCoroutine(CountDownToStart());

    }
    #endregion
    #region Timer
    IEnumerator CountDownToStart()
    {
        while (counter >= 0)
        {
            counterText.text = counter.ToString();
            yield return new WaitForSeconds(1f);
            counter--;
            if (counter == 0)
            {
                ActivateSuccessPanel();
            }
        }
    }
    #endregion

    #region Fruit and Bomb spawning
    private void StartSpawning()
    {
        StartCoroutine(SpawnPickUps());
    }
    IEnumerator SpawnPickUps()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        if (Random.Range(0, 10) >= 2)
        {
            Instantiate(fruitPickup, new Vector3(Random.Range(minX, maxX),
                                                Random.Range(minY, maxY), zPos),
                                                Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickup, new Vector3(Random.Range(minX, maxX),
                                                Random.Range(minY, maxY), zPos),
                                                Quaternion.identity);
        }

        Invoke("StartSpawning", 0f);
    }
    #endregion
    #region Increase Score
    public void IncreaseScore()
    {
        scoreCount++;
        scoreText.text = "Score: " + scoreCount;
    } 
    #endregion

    #region Buttons Events
    public void ContinueButton()
    {
        PlayButtonSound();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ActivateOptionsPanel()
    {
        PlayButtonSound();
        optionsPanel.SetActive(true);
    }

    public void ActivatePausePanel()
    {
        PlayButtonSound();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ActivateFailedPanel()
    {
        PlayButtonSound();
        failedPanel.SetActive(true);
        Time.timeScale = 0;
        CompareScores(0);
    }

    public void ActivateSuccessPanel()
    {
        PlayButtonSound();
        successPanel.SetActive(true);
        Time.timeScale = 0;
        CompareScores(1);
    }

    private void CompareScores(int index)
    {
        switch (index)
        {
            case 0:
                if (scoreCount >= highScore1)
                {
                    highScore3 = highScore2;
                    highScore2 = highScore1;
                    highScore1 = scoreCount;
                    highScoreText1.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3.text = "Player 3 : " + highScore3.ToString();
                    yourScoreText.text = "Your Score : " + scoreCount.ToString();
                    PlayerPrefs.SetInt("Player 1", highScore1);
                    PlayerPrefs.SetInt("Player 2", highScore2);
                    PlayerPrefs.SetInt("Player 3", highScore3);

                }
                else if (scoreCount >= highScore2)
                {
                    highScore3 = highScore2;
                    highScore2 = scoreCount;
                    highScoreText1.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3.text = "Player 3 : " + highScore3.ToString();
                    yourScoreText.text = "Your Score : " + scoreCount.ToString();
                    PlayerPrefs.SetInt("Player 2", highScore2);
                    PlayerPrefs.SetInt("Player 3", highScore3);
                }
                else if (scoreCount >= highScore3)
                {
                    scoreCount = highScore3;
                    highScoreText1.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3.text = "Player 3 : " + highScore3.ToString();
                    yourScoreText.text = "Your Score : " + scoreCount.ToString();
                    PlayerPrefs.SetInt("Player 3", highScore3);
                }
                else
                {
                    highScoreText1.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3.text = "Player 3 : " + highScore3.ToString();
                    yourScoreText.text = "Your Score : " + scoreCount.ToString();
                }
                break;
            case 1:
                if (scoreCount >= highScore1)
                {
                    highScore3 = highScore2;
                    highScore2 = highScore1;
                    highScore1 = scoreCount;
                    highScoreText1S.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2S.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3S.text = "Player 3 : " + highScore3.ToString();
                    yourScoreTextS.text = "Your Score : " + scoreCount.ToString();
                    PlayerPrefs.SetInt("Player 1", highScore1);
                    PlayerPrefs.SetInt("Player 2", highScore2);
                    PlayerPrefs.SetInt("Player 3", highScore3);

                }
                else if (scoreCount >= highScore2)
                {
                    highScore3 = highScore2;
                    highScore2 = scoreCount;
                    highScoreText1S.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2S.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3S.text = "Player 3 : " + highScore3.ToString();
                    yourScoreTextS.text = "Your Score : " + scoreCount.ToString();
                    PlayerPrefs.SetInt("Player 2", highScore2);
                    PlayerPrefs.SetInt("Player 3", highScore3);
                }
                else if (scoreCount >= highScore3)
                {
                    scoreCount = highScore3;
                    highScoreText1S.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2S.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3S.text = "Player 3 : " + highScore3.ToString();
                    yourScoreTextS.text = "Your Score : " + scoreCount.ToString();
                    PlayerPrefs.SetInt("Player 3", highScore3);
                }
                else
                {
                    highScoreText1S.text = "Player 1 : " + highScore1.ToString();
                    highScoreText2S.text = "Player 2 : " + highScore2.ToString();
                    highScoreText3S.text = "Player 3 : " + highScore3.ToString();
                    yourScoreTextS.text = "Your Score : " + scoreCount.ToString();
                }
                break;
            default:
                break;
        }
    }

    public void OpenCloseSounds()
    {
        if (isSoundActive)
        {
            isSoundActive = false;
            soundImage.sprite = soundSprites[1];
        }
        else if (!isSoundActive)
        {
            isSoundActive = true;
            soundImage.sprite = soundSprites[0];
        }
    }

    public void BackToPausePanel()
    {
        PlayButtonSound();
        optionsPanel.SetActive(false);
    }

    public void OpenScene(int index)
    {
        PlayButtonSound();
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }
    public void PlayButtonSound()
    {
        if (isSoundActive)
        {
            AudioSource.PlayClipAtPoint(buttonSound, Vector3.zero);
        }
    }
    #endregion


    #region Effects when the Player touching other objects.
    public void InstantiateEffects(int index, Vector3 pos)
    {
        switch (index)
        {
            case 0:
                GameObject newEatingEffect = Instantiate(eatingEffect, pos, Quaternion.identity);
                Destroy(newEatingEffect, 1f);
                break;
            case 1:
                GameObject newBombEffect = Instantiate(bombEffect, pos, Quaternion.identity);
                Destroy(newBombEffect, 1f);
                break;
            default:
                break;
        }
    } 
    #endregion

  
}
