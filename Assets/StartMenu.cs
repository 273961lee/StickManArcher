using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

public class StartMenu : MonoBehaviour {
    public EInt coins;
    public enum ArrowType
    {
        blackArrow=0,
        bloodArrow,
        iceArrow
    }
    public ArrowType arrowType = new ArrowType();
	// Use this for initialization
	void Start () {
        coins = PlayerPrefs.GetInt("coins");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeCoins(int cost) {
        coins -= cost;
        PlayerPrefs.SetInt("coins",coins);
        PlayerPrefs.Save();
    }

    public void SwitchArrow(int type) {
        arrowType = (ArrowType)type;
        PlayerPrefs.SetInt("arrowType",type);
    }
}
