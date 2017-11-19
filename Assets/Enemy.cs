using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int life = 10;
    public SpriteRenderer lifeBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int SubLife(int sublife) {
        life -= sublife;
        lifeBar.size = new Vector2(life*0.2f,0.3f);
        if (life<=0)
        {
            lifeBar.size = new Vector2(0,0.3f);
            Destroy(gameObject,3.0f);
        }
        return life;
    }
}
