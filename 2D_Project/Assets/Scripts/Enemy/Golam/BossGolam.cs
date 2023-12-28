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
            // �����ӿ� ���� �Ӽ� ����
            SetEnemyStatus("BossGolam", 100, 10, 1f, 1, 1.5f, 7f);
        }

        base.Start();
    }

    private void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        // �÷��̾���� �Ÿ��� ���� ���� ���� �ִ��� Ȯ��
        if (target != null && Vector2.Distance(transform.position, target.position) <= enemy.atkRange)
        {
            // ��Ÿ�� üũ
            if (currenttime <= 0)
            {
                ThrowStone();

                currenttime = cooltime;
            }
        }
        // ��Ÿ�� ����
        currenttime -= Time.fixedDeltaTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    private void ThrowStone()
    {
        // ���� �����ϰ� ������
        GameObject stoneCopy = Instantiate(Stone, pos.position, Quaternion.identity);
        Rigidbody2D stoneRigidbody = stoneCopy.GetComponent<Rigidbody2D>();

        if (stoneRigidbody != null)
        {
            enemyAnimator.SetTrigger("Stroke");

            // ���� �÷��̾� �������� ������
            Vector2 direction = (target.position - pos.position).normalized;
            stoneRigidbody.velocity = direction * enemy.atkSpeed;
        }
    }


protected override void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        spriteRenderer.flipX = (dir < 0);
        // ������ Enemy Ŭ������ MoveToTarget �޼��� ȣ��
        base.MoveToTarget();

        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }
}
