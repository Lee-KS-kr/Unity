using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float maxShotDelay;
    public float curshotDelay;

    public int life;
    public int score;
    public int power;
    public int maxPower;
    public int bomb;
    public int maxBomb;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isBoomTime;
    public bool isAlive;
    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA;
    public bool isButtonB;

    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject boomEffect;
    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject[] followers;
    public DynamicJoystick joyStick;


    Animator anim;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        isAlive = true;
    }

    void Update()
    {
        Move();
        Fire();
        Bomb();
        Reload();
    }

    //public void ButtonADown()
    //{
    //    isButtonA = true;
    //}

    //public void ButtonAUp()
    //{
    //    isButtonA = false;
    //}

    //public void ButtonBDown()
    //{
    //    isButtonB = true;
    //}

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
        {
            return;
        }
        //if (!isButtonA)
        //    return;

        if (curshotDelay < maxShotDelay)
        {
            return;
        }
        if (isAlive)
        {
            switch (power)
            {
                case 1:
                    GameObject bullet = objectManager.MakeObj("bulletPlayerA");
                    bullet.transform.position = transform.position;

                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
                case 2:
                    GameObject bulletR = objectManager.MakeObj("bulletPlayerA");
                    bulletR.transform.position = transform.position + Vector3.right * 0.15f;
                    GameObject bulletL = objectManager.MakeObj("bulletPlayerA");
                    bulletL.transform.position = transform.position + Vector3.left*0.15f;

                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
                default:
                    GameObject bulletRR = objectManager.MakeObj("bulletPlayerA");
                    bulletRR.transform.position = transform.position + Vector3.right * 0.3f;
                    GameObject bulletCC = objectManager.MakeObj("bulletPlayerB");
                    bulletCC.transform.position = transform.position;
                    GameObject bulletLL = objectManager.MakeObj("bulletPlayerA");
                    bulletLL.transform.position = transform.position + Vector3.left * 0.3f;

                    Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                    rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
            }
        }

        curshotDelay = 0;
    }

    void Reload()
    {
        curshotDelay += Time.deltaTime;
    }

    //public void JoyPanel(int type)
    //{
    //    for (int index = 0; index < 9; index++)
    //    {
    //        joyControl[index] = index == type;
    //    }
    //}

    //public void JoyDown()
    //{
    //    isControl = true;
    //}

    //public void JoyUp()
    //{
    //    isControl = false;
    //}


    void Move()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //horizontal = joyStick.Horizontal;
        //vertical = joyStick.Vertical;

        if ((horizontal == -1 && isTouchRight) || (horizontal == 1 && isTouchLeft) )
        {
            horizontal = 0;
        }

        if ((isTouchTop && vertical == 1) || (isTouchBottom && vertical == -1) )
        {
            vertical = 0;
        }

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal")
            || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)horizontal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("야야야 빠꾸해 빠꾸");
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            isAlive = false;
            anim.SetTrigger("isDie");
            boxCollider.enabled = false;

            life--;
            gameManager.UpdateLifeIcon(life);

            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                Invoke("YouDie", 1f);
                gameObject.SetActive(false);
            }
        }
        else if(collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1500;
                    break;
                case "Power":
                    if (power == maxPower)
                    {
                        score += 500;
                    }
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if (bomb == maxBomb)
                    {
                        score += 500;
                    }
                    else
                    {
                        bomb++;
                        gameManager.UpdateBombIcon(bomb);
                    }
                    break;
            }
            item.gameObject.SetActive(false);
        }
    }

    void AddFollower()
    {
        if (power == 4)
            followers[0].SetActive(true);
        else if (power == 5)
            followers[1].SetActive(true);
        else if (power == 6)
            followers[2].SetActive(true);
    }
    
    void Bomb()
    {
        if (!Input.GetButton("Fire2"))
            return;

        //if (!isButtonB)
        //    return;

        if (isBoomTime)
            return;

        if (bomb == 0)
            return;
        bomb--;
        isBoomTime = true;
        gameManager.UpdateBombIcon(bomb);
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 5);

        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        GameObject[] Boss = objectManager.GetPool("Boss");
        for (int index = 0; index < enemiesL.Length; index++)
        {
            if(enemiesL[index].activeSelf)
            {
                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(100);
            }
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {
                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(100);
            }
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {
                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(100);
            }
        }

        GameObject[] bulletsA = objectManager.GetPool("bulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("bulletEnemyB");
        GameObject[] bulletsBossA = objectManager.GetPool("bulletBossA");
        GameObject[] bulletsBossB = objectManager.GetPool("bulletBossB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            if (bulletsA[index].activeSelf)
                bulletsA[index].SetActive(false);
        }
        for (int index = 0; index < bulletsB.Length; index++)
        {
            if (bulletsB[index].activeSelf)
                bulletsB[index].SetActive(false);
        }
        for (int index = 0; index < bulletsBossA.Length; index++)
        {
            if (bulletsBossA[index].activeSelf)
                bulletsBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletsBossB.Length; index++)
        {
            if (bulletsBossB[index].activeSelf)
                bulletsBossB[index].SetActive(false);
        }
    }

    void OffBoomEffect()
    {
        boomEffect.gameObject.SetActive(false);
        isButtonB = false;
        isBoomTime = false;
    }

    void YouDie()
    {
        gameManager.RespawnPlayer();
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
