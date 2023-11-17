using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public LayerMask Ground;
    public bool isGrounded = false;
    public RaycastHit2D hit;
    public Vector3 Grounded;
    public static Vector3 movePosisiton;
    //Move �Լ�
    public float moveSpeed = 1f;
    public Vector3 BeforePo = new Vector3(0,0,0);
    //Jump �Լ�
    public float jumpForce = 5f;
    public bool isJumped = false;


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Grounded = hit.point;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, Ground);
        isGrounded = (hit.collider != null);
        BeforePo = transform.position;
        Move();
        Crouch();
        if (isGrounded && isJumped)
        {
            animator.SetTrigger("Jump");
            isJumped = false;
        }
        Jump();
        if (transform.position == BeforePo && isGrounded)
        {
            animator.SetInteger("walk", 0);
        }

    }
    void Move()
    {
        movePosisiton = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movePosisiton = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            if (isGrounded)
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
            if (isGrounded) { 
            animator.SetInteger("walk", 1);
            }
            else
            {
                animator.SetInteger("walk", 0);

            }


        }

        transform.position += movePosisiton * moveSpeed * Time.deltaTime;

    }
    void Crouch()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetTrigger("Crouch");
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                animator.SetTrigger("Crouch");
            }
        }
    }
    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetTrigger("Jump");
                isJumped = true;
            }
        }
    }

}
