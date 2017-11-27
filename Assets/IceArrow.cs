using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (collision.CompareTag("EnemyArrow")&&collision.GetComponent<Shoter>()!=null)
        {
            icePaticle.Play();
            Instantiate(ice,transform.position,transform.rotation);
        }
    }
}
