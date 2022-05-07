using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

//This class is attached Canvas at scene 0. 
public class EntryScene : MonoBehaviour
{
    #region Variables
    private float highScore1;
    private float highScore2;
    private float highScore3;

    public AudioClip buttonSound;

    [Header("Panels")]
    public GameObject rankPanel;
    public GameObject optionsPanel;

    [Header("HighScore Texts")]
    public TextMeshProUGUI highScoreText1;
    public TextMeshProUGUI highScoreText2;
    public TextMeshProUGUI highScoreText3;

    [Header("Sound")]
    public bool isSoundActive;
    public List<Sprite> soundSprites;
    public Image soundImage; 
    #endregion
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Init();
    }

    #region Definitions before the scene opens
    private void Init()
    {
        highScore1 = PlayerPrefs.GetInt("Player 1");
        highScore2 = PlayerPrefs.GetInt("Player 2");
        highScore3 = PlayerPrefs.GetInt("Player 3");

        highScoreText1.text = "Player 1 : " + highScore1.ToString();
        highScoreText2.text = "Player 1 : " + highScore2.ToString();
        highScoreText3.text = "Player 1 : " + highScore3.ToString();
    }
    #endregion

    #region Button events
    public void OpenRankPanel()
    {
        PlayButtonSound();
        rankPanel.SetActive(true);
    }

    public void OpenOptionsPanel()
    {
        PlayButtonSound();
        optionsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        PlayButtonSound();
        rankPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OpenGameScene(int index)
    {
        PlayButtonSound();
        SceneManager.LoadScene(index);
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
    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
    }
    public void PlayButtonSound()
    {
        if (isSoundActive)
        {
            AudioSource.PlayClipAtPoint(buttonSound, Vector3.zero);
        }
    } 
    #endregion
}
