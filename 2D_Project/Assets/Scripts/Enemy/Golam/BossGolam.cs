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
            SetEnemyStatus("BossGolam", 100, 10, 1f, 1, 1.5f, 7f);
        }

        base.Start();
    }

    private void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        // 플레이어와의 거리가 공격 범위 내에 있는지 확인
        if (target != null && Vector2.Distance(transform.position, target.position) <= enemy.atkRange)
        {
            // 쿨타임 체크
            if (currenttime <= 0)
            {
                ThrowStone();

                currenttime = cooltime;
            }
        }
        // 쿨타임 감소
        currenttime -= Time.fixedDeltaTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    private void ThrowStone()
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
    }


protected override void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        spriteRenderer.flipX = (dir < 0);
        // 기존의 Enemy 클래스의 MoveToTarget 메서드 호출
        base.MoveToTarget();

        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }
}
