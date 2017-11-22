using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    public int score = 0;
    public static GameMenu instance;
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
    public void GoHome() {
        SceneLoader.instance.LoadScene("StartMenu");
    }

    public void Close(GameObject which) {
        which.SetActive(false);
    }

    public void Restart() {
        SceneLoader.instance.LoadScene("MainScene");
    }
    public void ActiveSelf() {
        gameObject.SetActive(true);
    }

    public void Press(float args) {
        Time.timeScale = args;
    }
    public void AddScore(int value) {
        score += value;
        scoreText.text = score.ToString();
    }
}
