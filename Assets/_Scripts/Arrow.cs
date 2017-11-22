using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Myscripts {
    public class Arrow : MonoBehaviour
    {
        private bool isHit;
        private Rigidbody2D rig;
        public ParticleSystem blood;
        // Use this for initialization
        private void FixedUpdate()
        {
            if (gameObject.CompareTag("EnemyArrow"))
            {
                rig.AddTorque(Time.fixedDeltaTime * 1f, ForceMode2D.Impulse);
            }
            else
            {
                rig.AddTorque(Time.fixedDeltaTime * -20f, ForceMode2D.Impulse);
            }
        }
        private void OnEnable()
        {
            rig = GetComponent<Rigidbody2D>();
            rig.centerOfMass = new Vector2(1f, 0.5f);
            if (gameObject.CompareTag("Enemy"))
            {
                transform.Rotate(new Vector3(0, 0, Random.Range(-20, 21)));
            }
        }
        void Start()
        {
            isHit = false;
            Invoke("CheckHit", 3f);
        }
        void CheckHit()
        {
            if (!isHit)
            {
                Destroy(gameObject);
            }
        }
        // Update is called once per frame
        void Update()
        {
            //tempY1 = transform.position.y;
            //StartCoroutine(NextFramCheck());
            //if (tempY1 < tempY2)
            //{
            //    //Tween tw= transform.DORotate(new Vector3(0, 0,-30f), 3f);
            //}
            //if (tempY1 > tempY2)
            //{
            //    //transform.DORotate(new Vector3(0, 0, 60), 2f);
            //}
        }

        //IEnumerator NextFramCheck() {
        //    yield return new WaitForEndOfFrame();
        //    tempY2 = transform.position.y;
        //}
        private void OnCollisionEnter2D(Collision2D collision)
        {
            print("here");
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && gameObject.CompareTag("PlayerArrow"))
            {
                Instantiate(blood, transform.position, Quaternion.identity);
                isHit = true;
                this.GetComponent<Rigidbody2D>().simulated = false;
                transform.SetParent(collision.transform);
            }
            if (collision.CompareTag("Player") && gameObject.CompareTag("EnemyArrow"))
            {
                Instantiate(blood, transform.position, Quaternion.identity).Play();
                isHit = true;
                this.GetComponent<Rigidbody2D>().simulated = false;
                transform.SetParent(collision.transform);
            }
        }
    }

}
