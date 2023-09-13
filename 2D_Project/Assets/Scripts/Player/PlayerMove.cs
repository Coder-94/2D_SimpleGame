using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D Player2D;
    Player player = Player.GetInstance();
    SpriteRenderer rendering;
    Animator anime;
    public Vector3 position = new Vector3(0, 0,0 );
    public Vector3 arrow;     

    private void Start()
    {
        Player2D = GetComponent<Rigidbody2D>();
        rendering = gameObject.GetComponentInChildren<SpriteRenderer>();
        anime = GetComponent<Animator>();

    }
    private void Update()
    {

    }
    void PlayerMoving()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            arrow = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            arrow = Vector3.right;
        }if (arrow == Vector3.left)
        {
            transform.position = transform.position + arrow * 1 * Time.deltaTime;
        }
        else
        {
                transform.position = transform.position + arrow * -1 * Time.deltaTime;
        }


        }
}
