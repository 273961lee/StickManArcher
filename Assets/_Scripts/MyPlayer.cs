using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour {

    public int life;
    private SpriteRenderer lifeBar;
    public GameObject menu;
	// Use this for initialization
	void Start () {
        if (lifeBar==null)
        {
            lifeBar = transform.root.Find("Life").GetComponent<SpriteRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int SubLife(int subValue) {
        if (life>0)
        {
            life -= subValue;
            lifeBar.size = new Vector2(life*0.2f,0.3f);
        }
        else
        {
            Dead();
        }
        return life;
    }

    private void Dead() {
        menu.SetActive(true);
        gameObject.AddComponent<Rigidbody2D>().simulated = true;
    }
}
