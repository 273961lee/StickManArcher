using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Qarth;

public class GameMenu : MonoBehaviour {
    public int score = 0;
    public int coins = 0;
    public static GameMenu instance;
    public GameObject gameover;
    public Text scoreText;
    public Text gameOverTips;
    public Image powerBar;
    public float power;
    public bool isPowerFul;
    public bool closeSound;
    public AudioListener audioListener;
    public bool controlMod;
    public GameObject arch;
    public Transform archPos;
    [Multiline]
    public string link;
    public GameObject[] guides;
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

    public void LinkToGooglePlay() {
        Application.OpenURL(link);
    }

    public void SwitchControlMod() {
        if (PlayerPrefs.GetInt(PlayerData.CONTROL_MOD)==0)
        {
            PlayerPrefs.SetInt(PlayerData.CONTROL_MOD,1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>().controlMode = false;
            DrawLines.instance.isDefaultMod = false;
            print("切换控制模式");
        }
        else
        {
            PlayerPrefs.SetInt(PlayerData.CONTROL_MOD,0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>().controlMode = true;
            DrawLines.instance.isDefaultMod = true;
            print("切换控制模式");
        }
        PlayerPrefs.Save();
    }

    public void CloseSound()
    {
        audioListener.enabled = closeSound;
        closeSound = !closeSound;
    }

    private void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        audioListener = Camera.main.GetComponent<AudioListener>();
        arch = tempPlayer.transform.Find("ArmRM").gameObject;
        archPos = tempPlayer.transform.root.Find("ArchPos");
        print(archPos.name);
        if (!PlayerPrefs.HasKey(PlayerData.CONTROL_MOD))
        {
            PlayerPrefs.SetInt(PlayerData.CONTROL_MOD, 0);
        }
        GameObject temp= Instantiate(guides[PlayerPrefs.GetInt(PlayerData.CONTROL_MOD)]);
        Destroy(temp,1.5f);
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
    
    public void ResetPlayerAche() {
        arch.transform.eulerAngles = archPos.eulerAngles;
        arch.transform.position = archPos.position;
    }

    public void GameOver() {
        gameover.SetActive(true);
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent("PlayerScore",score);
        CraetEnemy.instance.isOn = false;
        gameOverTips.text = "You Got  " + score + "  score!   Try again?";
        coins = (int)(score*0.1f);
        int tempCoin = PlayerPrefs.GetInt(PlayerData.COINS)+coins;
        PlayerPrefs.SetInt(PlayerData.COINS,tempCoin);
        PlayerPrefs.Save();
    }
    public void GoHome() {
        SceneManager.LoadScene("StartMenu");
    }

    public void Share() {
        //share score
        Application.OpenURL(link);
    }

    public void Close(GameObject which) {
        which.SetActive(false);
    }

    public void Restart() {
        SceneManager.LoadScene("Loading");
    }
    public void ActiveSelf() {
        gameObject.SetActive(true);
    }

    public void Continue() {
        Time.timeScale = 1;
        ResetPlayerAche();
    }

    public void Quit() {
        SceneManager.LoadScene("StartMenu");
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
