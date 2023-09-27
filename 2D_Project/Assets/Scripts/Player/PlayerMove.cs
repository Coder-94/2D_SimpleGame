using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Animator animator;
    public Vector3 BeforePo = new Vector3(0,0,0);


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        BeforePo = transform.position;
        Move();
        if (transform.position == BeforePo)
        {
            animator.SetInteger("walk", 0);
        }
    }
    void Move()
    {
        Vector3 movePosisiton = Vector3.zero;
       
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movePosisiton = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetInteger("walk", 1);

        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            movePosisiton = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetInteger("walk", 1);


        }

        transform.position += movePosisiton * moveSpeed * Time.deltaTime;

    }


}
