using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Qarth;
using GameAnalyticsSDK;

public class GameMenu : MonoBehaviour {
    public Image left;
    public Image right;
    public Toggle controlButton;
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
    public int blackArrowNums;
    public int fireArrowNums;
    public Text fireArrow;
    public int iceArrowNums;
    public Text iceArrow;
    public Toggle[] arrow = new Toggle[3];
    public Shoter player;
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
        PlayerPrefs.SetInt(PlayerData.CONTROL_MOD,1);
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
            //print("切换控制模式");
        }
        else
        {
            PlayerPrefs.SetInt(PlayerData.CONTROL_MOD,0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>().controlMode = true;
            DrawLines.instance.isDefaultMod = true;
            //print("切换控制模式");
        }
        PlayerPrefs.Save();
    }

    public void CloseSound()
    {
        audioListener.enabled = !audioListener.enabled;
        if (audioListener.enabled == true)
        {
            PlayerPrefs.SetInt("Sound",1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound",0);
        }
    }
    //初始化箭的数量
    public void InitArrowNums() {
        fireArrowNums = PlayerPrefs.GetInt(PlayerData.BLOOD_ARROW);
        iceArrowNums = PlayerPrefs.GetInt(PlayerData.ICE_ARROW);
        fireArrow.text = fireArrowNums.ToString();
        iceArrow.text = iceArrowNums.ToString();
    }

    public void SwitchArrow(int whichOne) {
        if (player!=null)
        {
            player.SetArrow(whichOne);
        }
    }

    public void UpdateArrowNums() {
        if (player.GetArrow()==0)
        {

        }
        else if (player.GetArrow()==1)
        {
            fireArrowNums -= 1;
            fireArrow.text = fireArrowNums.ToString();
        }
        else
        {
            iceArrowNums -= 1;
            iceArrow.text = iceArrowNums.ToString();
        }
    }

    public void SavePlayerData() {
        int tempCoin = PlayerPrefs.GetInt(PlayerData.COINS) + coins;
        PlayerPrefs.SetInt(PlayerData.COINS, tempCoin);
        PlayerPrefs.SetInt(PlayerData.BLOOD_ARROW,fireArrowNums);
        PlayerPrefs.SetInt(PlayerData.ICE_ARROW, iceArrowNums);
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound",1);
        }
        if (PlayerPrefs.GetInt("Sound")==1)
        {
            audioListener.enabled = true;
        }
        else
        {
            audioListener.enabled = false;
        }
        GameAnalytics.NewDesignEvent("PlayGame",2);
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        audioListener = Camera.main.GetComponent<AudioListener>();
        arch = tempPlayer.transform.Find("ArmRM").gameObject;
        archPos = tempPlayer.transform.Find("ArchPos");
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
        InitArrowNums();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>();
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
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent("RestartGame");
        if (arch!=null)
        {
            arch.transform.eulerAngles = archPos.eulerAngles;
            arch.transform.position = archPos.position;
        }
    }

    public void GameOver() {
        gameover.SetActive(true);
        GameAnalytics.NewDesignEvent("GameOverWithScore",score);
        CraetEnemy.instance.isOn = false;
        gameOverTips.text = "You Got  " + score + "  score!   Try again?";
        coins = (int)(score*0.1f);
        SavePlayerData();
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
        Time.timeScale = 1.0f;
        player.isGamePlaying = true;
        ResetPlayerAche();
    }

    public void Quit() {
        SceneManager.LoadScene("StartMenu");
    }

    public void Press() {
        player.isGamePlaying = false;
        Time.timeScale = 0.00001f;
    }
    public void AddScore(int value) {
        score += value;
        PowerGrow();
        scoreText.text = score.ToString();
    }

    private void OnApplicationQuit()
    {
        GameAnalytics.NewDesignEvent("ExitGameWithPlayingTime",Time.time);
    }
}
