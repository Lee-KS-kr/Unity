using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public int enemyScore;
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    Animator anim;

    public float maxShotDelay;
    public float curshotDelay;

    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject player;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBomb;
    public ObjectManager objectManager;

    public string enemyName;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enemyName == "B")
            anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                health = 50; break;
            case "M":
                health = 15; break;
            case "S":
                health = 3; break;
            case "B":
                health = 5000;
                Invoke("Stop", 2);
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;
        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void Testt(Vector3 dir, float pos)
    {
        GameObject bullet = objectManager.MakeObj("bulletBossA");
        bullet.transform.position = transform.position + dir * pos;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        rigid.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
    }

    void FireFoward()
    {
        Testt(Vector3.right, 0.3f);
        Testt(Vector3.right, 0.9f);
        Testt(Vector3.left, 0.3f);
        Testt(Vector3.left, 0.9f);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
            Invoke("Think", 2);
    }

    void FireShot()
    {
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {

        GameObject bullet = objectManager.MakeObj("bulletBossB");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos((Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex])), -1);
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);


        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.1f);
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        int roundNumA = 40;
        int roundNumB = 35;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 2);
    }

    void Update()
    {
        if(enemyName == "B")
        {
            return;
        }
        Fire();
        Reload();
    }

    void Fire()
    {
        if (curshotDelay < maxShotDelay)
        {
            return;
        }

        if (enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("bulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.MakeObj("bulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            Vector3 dir = player.transform.position - bulletR.transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            bulletR.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 dirL = player.transform.position - bulletL.transform.position;
            float angleL = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            bulletL.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 3, ForceMode2D.Impulse);
        }

        curshotDelay = 0;
    }

    void Reload()
    {
        curshotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
            StartCoroutine("HitEnemy");

        if (health <= 0)
        {
            StopCoroutine("HitEnemy");
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
            gameObject.SetActive(false);
            Debug.Log("너는 이미 죽어있다.");
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            int ran = Random.Range(0, 10);
            switch (ran)
            {
                case 0:
                case 3:
                    GameObject itemBomb = objectManager.MakeObj("itemBoom");
                    itemBomb.transform.position = transform.position;
                    break;
                case 1:
                case 4:
                case 5:
                    GameObject itemCoin = objectManager.MakeObj("itemCoin");
                    itemCoin.transform.position = transform.position;
                    break;
                case 2:
                    GameObject itemPower = objectManager.MakeObj("itemPower");
                    itemPower.transform.position = transform.position;
                    break;
                default:
                    Debug.Log("No Item");
                    break;
            }
        }
    }

   IEnumerator HitEnemy()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        while (spriteRenderer.color != Color.white)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, 0.02f);
            yield return null;
        }
        yield return null;
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BorderBullet") && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            collision.gameObject.SetActive(false);
        }
    }
}
