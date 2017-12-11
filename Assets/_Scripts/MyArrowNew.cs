using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyArrowNew : MonoBehaviour {

    private Rigidbody2D father;
    private SpriteRenderer pic;
	// Use this for initialization
	void Start () {
        father = transform.parent.GetComponent<Rigidbody2D>();
        pic = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<SphereCollider>().enabled = false;
        if (!other.CompareTag("Player"))
        {
            father.simulated = false;
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Shoter>().ChangeLife(-4);
            }
        }
        
        Invoke("RemoveSelf",1.0f);
    }
    private void RemoveSelf() {
        pic.DOFade(0, 1f).OnComplete(() => Destroy(transform.parent.gameObject));
    }
}
