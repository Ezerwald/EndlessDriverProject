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

    public float elapsedTime;
    public float startTime;
    
    void Start()
    {
        startTime = Time.time;
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    public void UpdateTime()
    {
        elapsedTime = Time.time - startTime;
    }

    public void EndGame()
    {
        endGamePanel.SetActive(true);

        runTimeText.text = $"RUN TIME\n{Mathf.Round(elapsedTime)}";

        UpdateBestTime(Mathf.Round(elapsedTime));
        highScoreText.text = $"BEST TIME\n{PlayerPrefs.GetFloat("HighScore")}";
    }

    public void RestartGame()
    {
        startTime = Time.time;
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
