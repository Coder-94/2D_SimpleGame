using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //공격
    public float AttackSpeed = 3f;
    //공격 방향
    public Vector3 Direction;
    Rigidbody2D rb;

    private void Start()
    {
        if (PlayerAction.AttackPosistion == Vector3.right)
        {
            Direction = Vector3.right;
        }
        else
        {
            Direction = Vector3.left;
        }
    }
    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Direction * this.AttackSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            Destroy(this.gameObject);
        }
    }
}
