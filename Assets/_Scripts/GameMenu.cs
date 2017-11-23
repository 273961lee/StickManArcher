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
    public Text gameOverTips;
    public Image powerBar;
    public float power;
    public bool isPowerFul;
    // Use this for initialization
    void  Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        score = 0;
        gameover.SetActive(false);
        scoreText.text = "0";
        powerBar.fillAmount = 0.02f;
        power = 0;
    }
    public void PowerGrow() {
        if (score%10==0)
        {
            power += 0.1f;
        }
        if (power>=1)
        {
            power = 1;
            isPowerFul = true;
        }
        powerBar.fillAmount = power;
        powerBar.color += new Color(0.1f,0f,0f);
    }
    
    // Update is called once per frame
    void Update() {

    }

    public void GameOver() {
        gameover.SetActive(true);
        gameOverTips.text = "You Got  " + score + "  score!   Try again?";
    }
    public void GoHome() {
        SceneManager.LoadScene("MainScene");
    }

    public void Share() {
        //share score
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
        PowerGrow();
        scoreText.text = score.ToString();
    }
}
