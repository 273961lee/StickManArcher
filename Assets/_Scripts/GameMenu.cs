using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
    public int score = 0;
    public static GameMenu instance;
    public GameObject gameover;
    public Text scoreText;
    // Use this for initialization
    void Start() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void GameOver() {
        gameover.SetActive(true);
    }
    public void GoHome() {
        SceneManager.LoadScene("MainScene");
    }

    public void Close(GameObject which) {
        which.SetActive(false);
    }

    public void Restart() {
        SceneManager.LoadScene("MainScene");
    }
    public void ActiveSelf() {
        gameObject.SetActive(true);
    }

    public void Continue() {
        Time.timeScale = 1;
    }

    public void Quit() {
        Application.Quit();
    }

    public void Press() {
        Time.timeScale = 0.0001f;
    }
    public void AddScore(int value) {
        score += value;
        scoreText.text = score.ToString();
    }
}
