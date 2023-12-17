using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator enemyAnimator;
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

    public TemporaryFile player;

    public Transform target;
    float attackDelay;
    Enemy enemy;

    private void SetEnemyStatus(string _enemyName, int _maxHP, int _atkDmg, float _atkSpeed, 
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

    private void Start()
    {
        if(name.Equals("Enemy"))
        {
            SetEnemyStatus("Enemy", 100, 10, 1.5f, 2, 1.5f, 7f);
        }

        target = GameObject.FindWithTag("Archer").transform;
        SetAttackSpeed(atkSpeed);

        StartCoroutine(ThinkRoutine());
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        enemyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    IEnumerator ThinkRoutine()
    {
        while (true)
        {
            nextMove = Random.Range(-1, 2);

            enemyAnimator.SetInteger("WalkSpeed", nextMove);

            if (nextMove != 0)
            {
                spriteRenderer.flipX = nextMove == 1;
            }

            float nextThinkTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(nextThinkTime);
        }
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

    private void OnTriggerEnter2D(Collider2D col)
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

    void Update()
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

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        if (target.position.x - transform.position.x < 0)
        {
            spriteRenderer.flipX = (dir < 0);
        } else
        {
            spriteRenderer.flipX = (dir == 1);
        }

        transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }

    void AttackTarget()
    {
        //target.GetComponent<TemporaryFile>().nowHp -= enemy.atkDmg;
        enemyAnimator.SetTrigger("attack"); 
        attackDelay = enemy.atkSpeed; 
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}
