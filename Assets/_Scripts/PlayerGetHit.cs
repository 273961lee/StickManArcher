using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Myscripts;
namespace Myscripts {
    public class PlayerGetHit : MonoBehaviour
    {
        public int hitValue;
        public GameObject player;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("here");
            if (collision.CompareTag("EnemyArrow"))
            {
                collision.GetComponent<Rigidbody2D>().simulated = false;
                //player.GetComponent<Player>().SubLife(hitValue);
            }
        }
    }

}
