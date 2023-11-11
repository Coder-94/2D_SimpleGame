using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //�⺻ ����
    private Animator animator;
    private Rigidbody2D rb;
    //���� ����� Ȯ���ϴ� �μ�
    public bool isGround = false;
    //���� ��ġ Ȯ�� �μ�
    public Vector3 before = new Vector3(0, 0, 0);
    //Jump����
    public float jumpForce = 5f;
    public bool isJump = false;
    //Move ����
    public float moveSpeed = 5f;
    //Crouch ����
    public bool isCrouch = false;
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


    }
    private void FixedUpdate()
    {




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
        Vector3 movePosisiton = Vector3.zero;
        if (isCrouch == false)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                movePosisiton = Vector3.left;
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
}