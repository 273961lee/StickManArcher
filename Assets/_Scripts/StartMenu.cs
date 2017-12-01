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
    public Toggle[] toggles;
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
        GameAnalytics.NewDesignEvent("start log",1);
        UpdateCoins();
        GetArrowType();
        UpdateShop();
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
        toggles[1].interactable = coins > 20;
        toggles[2].interactable = coins > 40;
        toggles[0].isOn = false;
        toggles[1].isOn = false;
        toggles[2].isOn = false;
        toggles[(int)arrowType].isOn = true;
    }

    public void GetArrowType() {
        arrowType = (ArrowType)PlayerPrefs.GetInt(PlayerData.ARROW_TYPE);
    }
    public void SetArrowType(ArrowType type) {
        PlayerPrefs.SetInt(PlayerData.ARROW_TYPE,(int)type);
        arrowType = type;
        PlayerPrefs.Save();
    }
    public void SetArrowType(int type) {
        PlayerPrefs.SetInt(PlayerData.ARROW_TYPE, type);
        arrowType=(ArrowType)type;
        PlayerPrefs.Save();
    }

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
        }
    }

    public void UpdateCoins() {
        coins = PlayerPrefs.GetInt(PlayerData.COINS);
        coinsNum.text = coins.ToString();
    }

    public void ToMainScene() {
        SceneManager.LoadScene(scenesName[0]);
    }

    public void SwitchArrow(int type) {
        arrowType = (ArrowType)type;
        PlayerPrefs.SetInt(PlayerData.ARROW_TYPE,type);
    }
}
