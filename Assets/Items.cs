using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Items : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float temp1 = Screen.width;
        temp1 = Random.Range(temp1*0.5f,temp1);
        float temp2 = Screen.height;
        temp2 = Random.Range(1,temp2);
        Vector3 startPonit = Camera.main.ScreenToWorldPoint(new Vector3(temp1,temp2,0));
        startPonit += new Vector3(0,0,10);
        transform.position = startPonit;
        RemoveSelf(5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerArrow"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>().ChangeLife(2);
            RemoveSelf(0);
        }
        
    }

    private void RemoveSelf(int time) {
        transform.DOScale(transform.localScale * 1.5f, 0.2f).OnComplete(() => transform.DOScale(new Vector3(0, 0, 0),0.2f)).OnComplete(() => Destroy(gameObject,time));
    }
}
