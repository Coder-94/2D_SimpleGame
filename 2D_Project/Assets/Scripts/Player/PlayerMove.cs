using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    //Move 함수
    public float moveSpeed = 1f;
    public Vector3 BeforePo = new Vector3(0,0,0);
    //Jump 함수
    public bool isGrounded;
    public LayerMask Ground;
    public float jumpForce = 5f;
    public bool isJumped;



    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isJumped = false;
    }
    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, Ground);
        BeforePo = transform.position;
        Move();
        Jump();
        Crouch();
        if (transform.position == BeforePo)
        {
            animator.SetInteger("walk", 0);
        }
        if (isGrounded)
        {
            if (isJumped)
            {
                animator.SetTrigger("Jump");
            }
            isJumped = false;


        }

    }
    void Move()
    {
        Vector3 movePosisiton = Vector3.zero;
       
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movePosisiton = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            if (!isJumped)
            {
                animator.SetInteger("walk", 1);
            }
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            movePosisiton = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            if (!isJumped)
            {
                animator.SetInteger("walk", 1);
            }


        }

        transform.position += movePosisiton * moveSpeed * Time.deltaTime;

    }
    void Crouch()
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
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            isJumped = true;
        }
        
    }

}
