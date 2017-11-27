using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceArrow : MonoBehaviour {

    public GameObject ice;
    public ParticleSystem icePaticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyArrow"))
        {
            print("ICE SHOT");
            collision.GetComponent<Damage>().self.Dead();
            icePaticle.Play();
            GameObject tempIce= Instantiate(ice,collision.transform.parent.transform.position,Quaternion.identity);
            tempIce.GetComponent<SpriteRenderer>().DOColor(new Color(0, 0, 0, 0), 0.8f).OnComplete(() => Destroy(tempIce.gameObject));
        }
    }
}
