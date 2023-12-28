using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigid;
    protected Animator enemyAnimator;
    protected SpriteRenderer spriteRenderer;
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
    protected float attackDelay;
    protected Enemy enemy;

    Vector3 currentScale;


    protected virtual void Start()
    {

        target = GameObject.FindWithTag("Archer").transform;
        SetAttackSpeed(atkSpeed);

        currentScale = transform.localScale;


    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        enemyAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.FindWithTag("Archer").transform;
    }

    //Enemy 이동 & Raycast 체크
    protected void FixedUpdate()
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


    //Player에게 부딪혔을 때
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Archer"))
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

        if (attackDelay == 0)
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
    protected  void FaceTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        
        spriteRenderer.flipX = (targetDirection.x < 0);

        if (targetDirection.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1);
        }
    }

    //Player를 향해 이동
    protected virtual void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        spriteRenderer.flipX = (dir < 0);
        if (dir < 0)
        {
            Debug.Log("왼쪽에있음");
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1);
        }
        else
        {
            Debug.Log("오른쪽에있음");
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1);
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
        if (dir < 0)
        {
            //spriteRenderer.flipX = (dir < 0);
            transform.localScale = new Vector3(-currentScale.x, currentScale.y, 1);
        }
        else
        {
            //spriteRenderer.flipX = (dir == 1);
            transform.localScale = new Vector3(currentScale.x, currentScale.y, 1);
        }
    }

    //공격 속도 설정
    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }

    void Die()
    {
        //모든 실행중인 코루틴을 종료
        StopAllCoroutines();
        //죽는 코루틴 실행
        StartCoroutine(DieProcess());

        // 죽은 상태 처리하기 위한 코루틴
        IEnumerator DieProcess()
        {
            //일정 시간 대기 후 몬스터 제거
            yield return new WaitForSeconds(2f);
            Destroy(gameObject, 5f);
        }
        //enemyAnimator.SetTrigger("die");           
        //GetComponent<Enemy>().enabled = false;    
        //GetComponent<Collider2D>().enabled = false; 
        //Destroy(GetComponent<Rigidbody2D>());
        //Destroy(gameObject, 3);

    }

}
