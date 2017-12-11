using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RotatRound : MonoBehaviour {

    private Toggle father;
    private Shoter player;
	// Use this for initialization
	void Start () {
        father = transform.parent.GetComponent<Toggle>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>();
	}
	
	// Update is called once per frame
	void Update () {
        if (father.isOn)
        {
            transform.Rotate(new Vector3(0, 0, -90) * Time.deltaTime);
            switch (father.name)
            {
                case "Normal":
                    //player.SwitchArrow(0);
                    GameMenu.instance.SwitchArrow(0);
                    break;
                case "Fire":
                    //player.SwitchArrow(1);
                    GameMenu.instance.SwitchArrow(1);
                    break;
                case "Ice":
                    //player.SwitchArrow(2);
                    GameMenu.instance.SwitchArrow(2);
                    break;
                default:
                    break;
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
