using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour {
    public int life = 10;//bot生命值
    public SpriteRenderer lifeBar;//bot生命条
    public GameObject creatPonit;//出生点
	// Use this for initialization
	void Start () {
        creatPonit=GameObject.FindGameObjectWithTag("CreatPoint");
        transform.DOMove(creatPonit.transform.position,0.5f);
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
    private void OnDestroy()
    {
        GameMenu.instance.AddScore(10);
        if (creatPonit!=null)
        {
            creatPonit.SendMessage("CreatNext");
        }
    }
}
