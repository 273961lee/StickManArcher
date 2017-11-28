using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shoter : MonoBehaviour {
    public GameObject headIcon;//死亡骷髅头
    public Rigidbody2D[] rigs;//身体刚体
    public HingeJoint2D[] joints;//身体骨骼链
    public PolygonCollider2D[] collisions;//身体碰撞器
    public int life;//生命值
    public SpriteRenderer lifeBar;//生命条
    public GameObject[] items;//可刷新道具
    public ParticleSystem arrowRain;//箭雨
    public bool controlMode;
    public GameObject[] arrows;
    public GameObject arrow;
    public GameObject armM;//切换手臂模式
    public Transform aimPointRightHand;
    public bool isDead;
    public float shotSpeed;
    public List<Vector3> tempPoint = new List<Vector3>();
    public Vector3 shotDir;

    public bool isTouch = false;
    public Vector3 touchPoint;

    public GameObject arm;
    public Vector3 aimDir;
    public Transform aimPoint;
    public Transform arch;
    public Animator armMotion;

    public GameObject pool;

    public GameObject player;
    public GameObject powerArrow;
    public bool isPlayer;

    public GameObject arrowPrefab;
    public GameObject arrowTemp;

    public GameObject arrowTexture;

    public Vector3 aimDirRightHand;
    public float screenWight;
    public float screenHight;
    public GameObject midLine;
    public bool isGamePlaying;

    private IEnumerator CraetItem()
    {
        float timer = GameMenu.instance.score;
        timer = (200f-timer)*0.1f;
        if (timer<2)
        {
            timer = 2;
        }
        yield return new WaitForSeconds(timer);
        Instantiate(items[0],transform.position+new Vector3(10,10,0),Quaternion.identity);
        StartCoroutine(CraetItem());
    }

    //改变生命值
    public int ChangeLife(int value) {
        SoundBase.instance.PlayHitAU();
        life += value;
        if (life<0)
        {
            life = 0;
            Dead();
        }
        lifeBar.size = new Vector2(life*0.1f,0.2f);
        return life;
    }
    //是否死亡
    public bool Dead() {
        print(gameObject);
        print(life);
        SoundBase.instance.PlayDeadAU();
        SwichPhyics(false);
        if (isPlayer)
        {
            GameMenu.instance.GameOver();
        }
        if (!isPlayer)
        {
            headIcon.transform.DOMoveY(transform.position.y+3,1.0f).OnComplete(()=>headIcon.GetComponent<SpriteRenderer>().DOFade(0,0.2f));
            GameMenu.instance.AddScore(10);
            Destroy(gameObject, 2.0f);
        }
        return isDead = true;
    }

    public void ChangShotSpeed() {
        shotSpeed = 0;
        shotSpeed = Vector3.Distance(tempPoint[0],tempPoint[1])/Screen.width*35f;
    }

    public void Shot() {
        Rigidbody2D tempRig = arrowTemp.GetComponent<Rigidbody2D>();
        //print("get rig");
        shotDir = (Camera.main.ScreenToWorldPoint(touchPoint)+new Vector3(0,0,10)/*摄像机差值修正*/ - aimPoint.position).normalized;
        tempRig.transform.SetParent(pool.transform);
        tempRig.simulated = true;
        if (shotSpeed>22)
        {
            shotSpeed = 22;
        }
        tempRig.velocity = shotSpeed*shotDir;
        if (GameMenu.instance.isPowerFul)
        {
            arrowTemp = Instantiate(arrow, arrowTexture.transform.position, arrowTexture.transform.rotation);
            arrowTemp.transform.SetParent(arch);
        }
        else
        {
            arrowTemp = Instantiate(arrow, arrowTexture.transform.position, arrowTexture.transform.rotation);
            arrowTemp.transform.SetParent(arch);
        }
        //print("shot");
    }
    //shot mod ,true for right hand else is left hand
    public void Shot(bool defaultModle) {
        if (defaultModle==true)
        {
            Shot();
        }
        else
        {
            Rigidbody2D tempRig = arrowTemp.GetComponent<Rigidbody2D>();
            shotDir = (aimPointRightHand.position-Camera.main.ScreenToWorldPoint(touchPoint) + new Vector3(0, 0, 10)).normalized;
            tempRig.simulated = true;
            if (shotSpeed>12)
            {
                shotSpeed = 12;
            }
            print(shotSpeed);
            tempRig.velocity = shotSpeed * shotDir*3f;
            if (pool!=null)
            {
                tempRig.transform.SetParent(pool.transform);
            }
            //if (GameMenu.instance.isPowerFul)
            //{
            //    arrowTemp = Instantiate(arrow, arrowTexture.transform.position, arrowTexture.transform.rotation);
            //    arrowTemp.transform.SetParent(arch);
            //}
            //else
            //{
                arrowTemp = Instantiate(arrow, arrowTexture.transform.position, arrowTexture.transform.rotation);
                arrowTemp.transform.SetParent(arch);
            //}

        }
    }
    //enemy loopshot
    IEnumerator LoopShot() {
        /*float timer = 40 / GameMenu.instance.score;
        if (timer<1)
        {
            timer = 1;
        }*/
        arm.transform.DOLookAt(new Vector3(player.transform.position.x,player.transform.position.y+2,0),1.0f);
        yield return new WaitForSeconds(2);
        Shot(1);
        StartCoroutine(LoopShot());
    }
    //enemy shot function
    public void Shot(int type) {
        if (type==1)
        {
            Rigidbody2D tempRig = arrowTemp.GetComponent<Rigidbody2D>();
            shotDir = (player.transform.position - transform.position).normalized;
            tempRig.simulated = true;
            tempRig.velocity = Random.Range(15,25)*shotDir+(Vector3)Random.insideUnitCircle;
            tempRig.transform.SetParent(pool.transform);
            arrowTemp = Instantiate(arrowPrefab, arrowTexture.transform.position, arrowTexture.transform.rotation);
            arrowTemp.GetComponent<SpriteRenderer>().flipX = true;
            arrowTemp.transform.localScale = new Vector3(arrowTemp.transform.localScale.x, arrowTemp.transform.localScale.y, -arrowTemp.transform.localScale.z);
            arrowTemp.transform.SetParent(arch);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level!=-1)
        {
            isGamePlaying = true;
        }
    }
    // Use this for initialization
    void Start () {
        isGamePlaying = true;
        if (PlayerPrefs.GetInt("controlMod")==0)
        {
            controlMode = true;
            print("右手控制模式");
        }
        else
        {
            controlMode = false;
            print("左手控制模式");
        }
        screenWight = Screen.width;
        screenHight = Screen.height;
        isPlayer = gameObject.CompareTag("Player");
        if (isPlayer)
        {
            arrow = arrows[2];
        }
        print(PlayerPrefs.GetInt("arrowType"));
        player = GameObject.FindGameObjectWithTag("Player");
        pool = GameObject.FindGameObjectWithTag("Pool");
        midLine = GameObject.Find("Line");
        SwichPhyics(true);
        if (!isPlayer)
        {
            StartCoroutine(LoopShot());
        }
        else
        {
            StartCoroutine(CraetItem());
        }
        //print(isPlayer);
	}

    public void SwichPhyics(bool args) {
        foreach (HingeJoint2D item in joints)
        {
            item.enabled = !args;
            //if (!isPlayer)
            //{
            //    item.useMotor = !args;
            //    JointMotor2D motor = item.motor;
            //    motor.motorSpeed = Random.Range(-30, 30);
            //    item.motor = motor;
            //}
        }
        foreach (Rigidbody2D item in rigs)
        {
            if (args==true)
            {
                item.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                item.bodyType = RigidbodyType2D.Dynamic;
            }
            
        }
        foreach (PolygonCollider2D item in collisions)
        {
            item.isTrigger = args;
        }
    }
    // Update is called once per frame
    void Update () {
        if (isGamePlaying)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                arrow = arrows[0];
                PlayerPrefs.SetInt("arrowType", 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                arrow = arrows[1];
                PlayerPrefs.SetInt("arrowType", 1);

            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                arrow = arrows[2];
                PlayerPrefs.SetInt("arrowType", 2);

            }
            if (isPlayer && !isDead)
            {
                //test function
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Dead();
                }
                //Mathf.Clamp(arm.transform.rotation.eulerAngles.x,60,-30);
                touchPoint = Input.mousePosition;
                if (controlMode)
                {
                    aimDir = Camera.main.ScreenToWorldPoint(touchPoint);
                    aimDir = new Vector3(aimDir.x, aimDir.y, 0);
                    if (aimDir.x > midLine.transform.position.x)
                    {
                        aimDir = new Vector3(Mathf.Clamp(aimDir.x, midLine.transform.position.x, int.MaxValue), aimDir.y, aimDir.z);
                    }
                }
                else
                {
                    aimDir = Camera.main.ScreenToWorldPoint(touchPoint);
                    aimDir = new Vector3(aimDir.x, aimDir.y, 0);
                    if (aimDir.x < midLine.transform.position.x)
                    {
                        aimDir = new Vector3(Mathf.Clamp(aimDir.x, int.MinValue, midLine.transform.position.x), aimDir.y, aimDir.z);
                    }
                }
                //aimDir = new Vector3(Mathf.Clamp(aimDir.x,transform.position.x,int.MaxValue),aimDir.y,0);
                Ray2D ray = new Ray2D(touchPoint, aimPointRightHand.position);
                aimDirRightHand = ray.direction;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    tempPoint.Clear();
                    tempPoint.Add(touchPoint);
                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    isTouch = true;
                    if (aimDir.x < midLine.transform.position.x && !controlMode)
                    {
                        armMotion.SetBool("ArmMotion", true);
                        Aim(false);
                    }
                    if (aimDir.x > midLine.transform.position.x && controlMode)
                    {
                        armMotion.SetBool("ArmMotion", true);
                        Aim(true);
                    }
                }
                if (Input.GetMouseButtonUp(0) && isTouch)
                {
                    tempPoint.Add(touchPoint);
                    ChangShotSpeed();

                    if (aimDir.x < midLine.transform.position.x && !controlMode)
                    {
                        armMotion.SetBool("ArmMotion", false);
                        Shot(false);
                    }
                    if (aimDir.x > midLine.transform.position.x && controlMode)
                    {
                        armMotion.SetBool("ArmMotion", false);
                        Shot(true);
                    }
                    if (GameMenu.instance.score == 100)
                    {
                        GameObject.FindGameObjectWithTag("BackGround").GetComponent<ColorSetter>().ChangeColor();
                        /*arrowRain.Play();
                        List<GameObject> enemys = new List<GameObject>();
                        enemys.AddRange(GameObject.FindGameObjectsWithTag("EnemyArrow"));
                        for (int i = 0; i < enemys.Count-1; i++)
                        {
                            if (enemys[i].GetComponent<Shoter>()!=null)
                            {
                                enemys[i].GetComponent<Shoter>().Dead();
                            }
                        }*/
                    }
                    //print("all rise shot");
                    isTouch = false;
                }
            }
        }

    }
    public void Aim(int args) {
        arm.transform.LookAt(-aimDir);
    }
    public void Aim() {
        arm.transform.LookAt(aimDir);
    }
    public void Aim(bool defaultModle) {
        if (defaultModle)
        {
            Aim();
        }
        else
        {
            //arm.transform.SetParent(armM.transform);
            //armM.transform.eulerAngles = new Vector3(touchPoint.normalized.x*-90f,90,0);
            armM.transform.LookAt(aimDir);
        }
    }
}
