using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyArrow : MonoBehaviour {
    private bool isHit;
    private Rigidbody2D rig;
    private SpriteRenderer selfPic;
    public ParticleSystem blood;
    // Use this for initialization
    private void FixedUpdate()
    {
        if (gameObject.CompareTag("EnemyArrow"))
        {
            rig.AddTorque(Time.fixedDeltaTime * 0.3f, ForceMode2D.Impulse);
        }
        else
        {
            rig.AddTorque(Time.fixedDeltaTime * -0.3f, ForceMode2D.Impulse);
        }
    }
    private void OnEnable()
    {
        selfPic = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("EnemyArrow"))
        {
            rig.centerOfMass = new Vector2(0f, 0.5f);
        }
        else
        {
            rig.centerOfMass = new Vector2(1f, 0.5f);
        }
        if (blood == null)
        {
            blood = GetComponentInChildren<ParticleSystem>();
        }
    }
    private void Start()
    {
        if (GameMenu.instance.isPowerFul&&gameObject.CompareTag("PlayerArrow"))
        {
            gameObject.AddComponent<PowerArrow>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(gameObject.tag))
        {
            if (collision.CompareTag("EnemyArrow")||collision.CompareTag("PlayerArrow"))
            {
                transform.SetParent(collision.transform);
                if (blood!=null)
                {
                    blood.Play();
                }
            }
            rig.simulated = false;
            Invoke("RemoveSelf",2.0f);
        }
    }
    private void RemoveSelf() {
        selfPic.DOFade(0,1f).OnComplete(()=>Destroy(gameObject));
    }
}
