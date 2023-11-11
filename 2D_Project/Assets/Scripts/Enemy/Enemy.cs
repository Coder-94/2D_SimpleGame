using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    public TemporaryFile player; //플레이어 임시 스크립트 (나중에 삭제할 예정)

    public Transform target;
    float attackDelay;
    Enemy enemy;
    Animator enemyAnimator;

    private void SetEnemyStatus(string _enemyName, int _maxHP, int _atkDmg, int _atkSpeed, 
       float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        maxHp = _maxHP;
        nowHp = _maxHP;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5);
    }

    void FixedUpdate()
     {
         rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

         Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
         Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

         RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

         if (rayHit.collider == null)
         {
            Turn();
        }
     }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        anim.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove *= -1;
        spriteRenderer.flipX = (nextMove == 1);

        CancelInvoke();
        Invoke("Think", 5);
    }
    /*void Die()
    {
        enemyAnimator.SetTrigger("die");           
        GetComponent<Enemy>().enabled = false;    
        GetComponent<Collider2D>().enabled = false; 
        Destroy(GetComponent<Rigidbody2D>());  
        Destroy(gameObject, 3);                   
    }*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(player.attacked)
            {
                nowHp -= player.atkDmg;
                player.attacked = false;
                if(nowHp<=0) //적 사망
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void Update()
    {
        attackDelay -= Time.deltaTime; //time.deltaTIme: 1프레임당 걸리는 시간
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        // 공격 딜레이가 0일때, 시야 범위 안에 들어올 때
        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            FaceTarget();

            // 공격 범위 안에 들어오면 공격
            if (distance <= enemy.atkRange)
            {
                AttackTarget();
            }
            else
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else
        {
            anim.SetInteger("WalkSpeed", nextMove);
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime);
        enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void AttackTarget()
    {
        target.GetComponent<TemporaryFile>().nowHp -= enemy.atkDmg;
        enemyAnimator.SetTrigger("attack"); 
        attackDelay = enemy.atkSpeed; 
    }
   
}
