using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolam : Enemy
{
    public float distance;
    public float atkdistance;
    public LayerMask isLayer;
    public float speed;

    public GameObject Stone;
    public Transform pos;

    public float cooltime;
    private float currenttime;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (name.Equals("BossGolam"))
        {
            // 슬라임에 대한 속성 설정
            SetEnemyStatus("BossGolam", 100, 10, 1, 1.5f, 7f);
        }

        base.Start();
    }

    private void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {

            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkdistance)
            {
                if (currenttime <= 0)
                {
                    GameObject Stonecopy = Instantiate(Stone, pos.position, transform.rotation);

                    currenttime = cooltime;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

/*    private void ThrowStone()
    {
        // 돌을 생성하고 던지기
        GameObject stoneCopy = Instantiate(Stone, pos.position, Quaternion.identity);
        Rigidbody2D stoneRigidbody = stoneCopy.GetComponent<Rigidbody2D>();

        if (stoneRigidbody != null)
        {
            enemyAnimator.SetTrigger("Stroke");

            // 돌을 플레이어 방향으로 던지기
            Vector2 direction = (target.position - pos.position).normalized;
            stoneRigidbody.velocity = direction * enemy.atkSpeed;
        }
    }*/


protected override void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        // 기존의 Enemy 클래스의 MoveToTarget 메서드 호출
        base.MoveToTarget();

        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }
}
