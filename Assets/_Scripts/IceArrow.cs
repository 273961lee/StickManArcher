using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceArrow : MonoBehaviour {

    public GameObject ice;
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
        }
    }
}
