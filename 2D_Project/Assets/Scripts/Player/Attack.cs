using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //АјАн
    public float attackPower = 5f;
    public Rigidbody2D AttackO;
    public GameObject Player1;
    public float attackTime = 0f;
    public Vector3 movePosisiton;

    private void Start()
    {

        AttackO = gameObject.GetComponent<Rigidbody2D>();
        
        if(PlayerMove.movePosisiton == Vector3.right)
        {
            movePosisiton = Vector3.right;
        }
        else
        {
            movePosisiton = Vector3.left;
        }
    }
    private void OnEnable()
    {
        transform.position = Player1.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    public void Move()
    {
        if (attackTime >= 5)
        {
            Destroy(this.gameObject);
        }
        AttackO.AddForce(movePosisiton * attackPower);
        attackTime += Time.deltaTime;
    }
