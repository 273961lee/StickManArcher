using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public int subLifeValue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerArrow"))
        {
            if (transform.parent.GetComponent<Enemy>().SubLife(subLifeValue)<=0)
            {
                transform.parent.gameObject.AddComponent<Rigidbody2D>().simulated = true;
                this.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
