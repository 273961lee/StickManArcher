using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour {
    private float tempY1;
    private float tempY2;
    private bool isHit;
    // Use this for initialization
    private void OnEnable()
    {
        transform.Rotate(new Vector3(0,0,Random.Range(-20,21)));
    }
    void Start () {
        isHit = false;
	}
	
	// Update is called once per frame
	void Update () {
        tempY1 = transform.position.y;
        StartCoroutine(NextFramCheck());
        if (tempY1 < tempY2)
        {
            //Tween tw= transform.DORotate(new Vector3(0, 0,-30f), 3f);
        }
        if (tempY1 > tempY2)
        {
            //transform.DORotate(new Vector3(0, 0, 60), 2f);
        }
    }

    IEnumerator NextFramCheck() {
        yield return new WaitForEndOfFrame();
        tempY2 = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&&gameObject.CompareTag("PlayerArrow"))
        {
            isHit = true;
            this.GetComponent<Rigidbody2D>().simulated = false;
            transform.SetParent(collision.transform);
        }
        if (collision.CompareTag("Player") && gameObject.CompareTag("EnemyArrow"))
        {
            isHit = true;
            this.GetComponent<Rigidbody2D>().simulated = false;
            transform.SetParent(collision.transform);
        }
    }
}
