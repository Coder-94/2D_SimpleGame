using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //�⺻ ����
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject Player;
    //���� ����� Ȯ���ϴ� �μ�
    public bool isGround = false;
    public string PlayerName = "";
    //���� ��ġ Ȯ�� �μ�
    public Vector3 before = new Vector3(0, 0, 0);
    //Jump����
    public float jumpForce = 5f;
    public bool isJump = false;
    //Move ����
    public float moveSpeed = 5f;
    //Crouch ����
    public bool isCrouch = false;
    //Attack ����
    public GameObject aObject;
    public bool isAttack = false;
    public float AttackTime = 0f;
    public float attackDelay = 3f;
    public static Vector3 AttackPosistion = Vector3.right;
    public Vector3 movePosisiton;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerName = gameObject.name;
        if(PlayerName == "Archer")
        {
            attackDelay = 0.7f;
        }else if(PlayerName == "Wizard")
        {
            attackDelay = 0.5f;
        }
        else
        {
            attackDelay = 0.7f;
        }

    }
    private void Update()
    {
        Debug.Log(PlayerName);
        before = transform.position;
        if (before == transform.position)
        {
            animator.SetInteger("walk", 0);
        }
        Crouch();
        Move();
        Jump();
        Attack();
        if (isAttack == true)
        {
            AttackTime += Time.deltaTime;
                if (AttackTime >= attackDelay)
                {
                    isAttack = false;
                    animator.SetBool("Attack", false);
                    AttackTime = 0f;
                }
            }
        }
    



    private void OnCollisionStay2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Ground"))
        {
            isGround = true;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Ground"))
        {
            if (isJump == true)
            {
                animator.SetBool("Jump", false);
                isJump = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
    private void Move()
    {
        movePosisiton = Vector3.zero;

        if (isCrouch == false && isAttack == false)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                movePosisiton = Vector3.left;
                AttackPosistion = Vector3.left;
                transform.localScale = new Vector3(-1, 1, 1);
                if (isGround)
                {
                    animator.SetInteger("walk", 1);
                }
                else
                {
                    animator.SetInteger("walk", 0);

                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                movePosisiton = Vector3.right;
                AttackPosistion = Vector3.right;
                transform.localScale = new Vector3(1, 1, 1);
                if (isGround)
                {
                    animator.SetInteger("walk", 1);
                }
                else
                {
                    animator.SetInteger("walk", 0);
                }
            }
            transform.position += movePosisiton * moveSpeed * Time.deltaTime;

        }
    }
    private void Crouch()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetBool("Crouch", true);
                isCrouch = true;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                animator.SetBool("Crouch", false);
                isCrouch = false;
            }
        }
    }
    private void Jump()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("Jump", true);
                isJump = true;
                if (isCrouch)
                {
                    isCrouch = false;
                    animator.SetBool("Crouch", false);
                }
            }
        }

    }
    private void Attack()
    {
        if (isGround)
            if (Input.GetKeyDown(KeyCode.X) && AttackTime == 0f)
            {
                if (PlayerName == "Archer")
                {
                    StartCoroutine(AtcherAttack());
                }
                else if (PlayerName == "Wizard")
                {
                    StartCoroutine(WizardAttack());
                }
            }
    }
    IEnumerator AtcherAttack()
    {
        animator.SetBool("Attack", true);
        isAttack = true;
        yield return new WaitForSeconds(0.7f);
        var bulletGo = Instantiate<GameObject>(this.aObject);
        bulletGo.transform.position = this.transform.position;
        yield break;
    }
    IEnumerator WizardAttack()
    {
        animator.SetBool("Attack", true);
        isAttack = true;
        yield return new WaitForSeconds(0.2f);
        var bulletGo = Instantiate<GameObject>(this.aObject);
        bulletGo.transform.position = this.transform.position;
        yield break;
    }
}
