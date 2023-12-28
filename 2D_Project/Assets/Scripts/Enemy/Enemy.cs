using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator enemyAnimator;
    public SpriteRenderer spriteRenderer;
    public int nextMove;

    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;

    public float fieldOfVision;

    public TemporaryFile player;

    public Transform target;
    float attackDelay;
    Enemy enemy;

    //Enemy 속성 설정
    protected void SetEnemyStatus(string _enemyName, int _maxHP, int _atkDmg, float _atkSpeed, 
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

    protected virtual void Start()
    {
        if(name.Equals("Enemy"))
        {
            SetEnemyStatus("Enemy", 100, 10, 1.5f, 2, 1.5f, 7f);
        }

        SetAttackSpeed(atkSpeed);

        StartCoroutine(ThinkRoutine());
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        enemyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.FindWithTag("Archer").transform;
    }

    //Enemy 무작위 이동 로직을 담은 코루틴
    IEnumerator ThinkRoutine()
    {
        while (true)
        {
            MoveToTarget();
            yield return null;
        }
    }
    //Enemy 이동 & Raycast 체크
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

    //Enemy 방향 전환
    void Turn()
    {
        nextMove = nextMove *= -1;
        spriteRenderer.flipX = (nextMove == 1);
    }

    void Die()
    {
        enemyAnimator.SetTrigger("die");           
        GetComponent<Enemy>().enabled = false;    
        GetComponent<Collider2D>().enabled = false; 
        Destroy(GetComponent<Rigidbody2D>());  
        Destroy(gameObject, 3);                   
    }

    //Player에게 부딪혔을 때
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(player.attacked)
            {
                nowHp -= player.atkDmg;
                player.attacked = false;
                if(nowHp<=0)
                {
                    Die();
                }
            }
        }
    }

    protected virtual void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            FaceTarget();

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
            enemyAnimator.SetInteger("WalkSpeed", 0);

        }
    }

    //Player 바라보기
    void FaceTarget()
    {
        Vector3 targetDirection = target.position - transform.position;

        spriteRenderer.flipX = (targetDirection.x < 0);

        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //Player를 향해 이동
    protected virtual void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;



        spriteRenderer.flipX = (dir < 0);
        if (target.position.x - transform.position.x < 0)
        {
            spriteRenderer.flipX = (dir < 0);
        }
        else
        {
            spriteRenderer.flipX = (dir == 1);
        }


        transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime);
            enemyAnimator.SetInteger("WalkSpeed", (int)dir);
      }

    //Player 공격
    void AttackTarget()
    {
        enemyAnimator.SetTrigger("attack"); 
        attackDelay = enemy.atkSpeed;

        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        if (target.position.x - transform.position.x < 0)
        {
            spriteRenderer.flipX = (dir < 0);
        }
        else
        {
            spriteRenderer.flipX = (dir == 1);
        }
    }

    //공격 속도 설정
    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }


}
