using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public EInt coins;
    public Text coinsNum;
    public GameObject[] arrowSet;
    private int index;
    public GameObject settingMenu;
    public GameObject shopMenu;

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
        UpdateCoins();
        PlayerPrefs.SetInt("arrowType",SetArrow());
        for (int i = 0; i < arrowSet.Length; i++)
        {
            arrowSet[i].SetActive(!PlayerPrefs.GetInt("arrowType").Equals(i));
        }
	}

    public int SetArrow() {
        for (int i = 0; i < arrowSet.Length; i++)
        {
            if (arrowSet[i].activeInHierarchy)
            {
                index = i;
            }
        }
        return index;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeCoins(int cost) {
        if (coins<cost)
        {

        }
        else
        {
            coins -= cost;
            coinsNum.text = coins.ToString();
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.Save();
        }
    }

    public void UpdateCoins() {
        coins = PlayerPrefs.GetInt("coins");
        coinsNum.text = coins.ToString();
    }

    public void SwitchArrow(int type) {
        arrowType = (ArrowType)type;
        PlayerPrefs.SetInt("arrowType",type);
    }
}
