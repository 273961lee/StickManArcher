using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class StartMenu : MonoBehaviour {
    public EInt coins;
    public Text coinsNum;
    private int index;
    public GameObject settingMenu;
    public GameObject shopMenu;
    public string[] scenesName;
    public Button[] toggles;
    private int[] arrowNums=new int[2];
    public enum ArrowType
    {
        blackArrow=0,
        bloodArrow,
        iceArrow
    }
    public enum MenuState
    {
        closed=0,
        shop=1,
        setting
    }

    public MenuState menuState = new MenuState();
    public ArrowType arrowType = new ArrowType();
	// Use this for initialization
	void Start () {
        GameAnalytics.NewDesignEvent("Start_Scene",1);
        AppsFlyer.trackEvent("","MainGame");
        if (!PlayerPrefs.HasKey(PlayerData.BLOOD_ARROW))
        {
            PlayerPrefs.SetInt(PlayerData.BLOOD_ARROW,0);
        }
        arrowNums[0] = PlayerPrefs.GetInt(PlayerData.BLOOD_ARROW);
        arrowNums[1] = PlayerPrefs.GetInt(PlayerData.ICE_ARROW);
        UpdateCoins();
        //GetArrowType();
        UpdateShop();
        print("coins :"+coins);
	}
    

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeCoins(-100);
            UpdateCoins();
        }
    }

    public void UpdateShop() {
        if (coins>=20&&coins<40)
        {
            toggles[0].interactable = true;
        }
        else if (coins>=40)
        {
            toggles[1].interactable = true;
        }
        else
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = false;
            }
        }
    }

    //public void GetArrowType() {
    //    arrowType = (ArrowType)PlayerPrefs.GetInt(PlayerData.ARROW_TYPE);
    //}
    //public void SetArrowType(ArrowType type) {
    //    PlayerPrefs.SetInt(PlayerData.ARROW_TYPE,(int)type);
    //    arrowType = type;
    //    PlayerPrefs.Save();
    //}
    //public void SetArrowType(int type) {
    //    PlayerPrefs.SetInt(PlayerData.ARROW_TYPE, type);
    //    arrowType=(ArrowType)type;
    //    PlayerPrefs.Save();
    //}

    public void ChangeCoins(int cost) {
        if (coins<cost)
        {

        }
        else
        {
            coins -= cost;
            coinsNum.text = coins.ToString();
            PlayerPrefs.SetInt(PlayerData.COINS, coins);
            PlayerPrefs.Save();
            UpdateCoins();
        }
    }

    public void UpdateCoins() {
        coins = PlayerPrefs.GetInt(PlayerData.COINS);
        coinsNum.text = coins.ToString();
    }

    public void ToMainScene() {
        SceneManager.LoadScene(scenesName[0]);
    }

    public void AddArrows(int type) {
        arrowNums[type] += 20;
        switch (type)
        {
            case 0:
                PlayerPrefs.SetInt(PlayerData.BLOOD_ARROW, arrowNums[type]);
                break;
            case 1:
                PlayerPrefs.SetInt(PlayerData.ICE_ARROW, arrowNums[type]);
                break;
            default:
                break;
        }
        UpdateShop();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
