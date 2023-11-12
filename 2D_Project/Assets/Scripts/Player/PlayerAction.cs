using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //기본 사항
    private Animator animator;
    private Rigidbody2D rb;
    //땅에 닿는지 확인하는 인수
    public bool isGround = false;
    //기존 위치 확인 인수
    public Vector3 before = new Vector3(0, 0, 0);
    //Jump인자
    public float jumpForce = 5f;
    public bool isJump = false;
    //Move 인자
    public float moveSpeed = 5f;
    //Crouch 인자
    public bool isCrouch = false;
    //Attack 인자
    public GameObject aObject;
    public bool isAttack = false;
    public float AttackTime = 0f;
    public static Vector3 AttackPosistion = Vector3.right;
    public Vector3 movePosisiton;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        before = transform.position;
        if (before == transform.position)
        {
            animator.SetInteger("walk", 0);
        }
        Crouch();
        Move();
        Jump();
        Attack();
        if(isAttack == true)
        {
            AttackTime += Time.deltaTime;
        }
        if (AttackTime >= 0.6f)
        {
            isAttack = false;
            animator.SetBool("Attack", false);
            AttackTime = 0f;
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
                isJump= true;
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
                animator.SetBool("Attack", true);
                var bulletGo = Instantiate<GameObject>(this.aObject);
                bulletGo.transform.position = this.transform.position;
                isAttack = true;

            }
    }
}
