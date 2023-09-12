using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 0.5f;
    Rigidbody2D Player2D;
    Player player = Player.GetInstance();
    SpriteRenderer rendering;
    Animator anime;
    public Vector3 position = new Vector3(0, 0,0 );

    private void Start()
    {
        Player2D = GetComponent<Rigidbody2D>();
        rendering = gameObject.GetComponentInChildren<SpriteRenderer>();
        anime = GetComponent<Animator>();

    }
    private void Update()
    {
        PlayerMoving();
        if (Input.GetKey(KeyCode.Space))
        {
            anime.SetInteger("walk", 0);
        }
    }
    void PlayerMoving()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.right;
            rendering.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            rendering.flipX = false;
        }
            if (Input.GetKey(KeyCode.RightArrow))
        {
            Player2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
            anime.SetInteger("walk", 2);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
            anime.SetInteger("walk", 2);
        }


    }
}
