using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    public TMP_Text timeText;

    public GameObject endGamePanel;
    public TMP_Text runTimeText;
    public TMP_Text highScoreText;

    [SerializeField] HoverMotor hoverMotor;
    public float currentTime = 0;
    
    void Start()
    {
        endGamePanel.SetActive(false);
    }

    public void EndGame()
    {
        currentTime = hoverMotor.GetCurrentTime();

        endGamePanel.SetActive(true);

        runTimeText.text = $"RUN TIME\n{Mathf.Round(currentTime)}";

        UpdateBestTime(Mathf.Round(currentTime));
        highScoreText.text = $"BEST TIME\n{PlayerPrefs.GetFloat("HighScore")}";
    }

    public void RestartGame()
    {
        hoverMotor.SetStartTime(Time.time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateBestTime(float newValue)
    {
        float currentValue = PlayerPrefs.GetFloat("HighScore", 0);
        if (currentValue < newValue)
        {
            PlayerPrefs.SetFloat("HighScore", newValue);
        }
    }

}
