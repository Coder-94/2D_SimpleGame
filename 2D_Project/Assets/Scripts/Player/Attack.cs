using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //����
    public float AttackSpeed = 3f;
    //���� ����
    public Vector3 Direction;
    Rigidbody2D rb;
    public float DestroyTime = 0f;

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
        DestroyTime += Time.deltaTime;
        if (DestroyTime >= 3f)
        {
            Debug.Log("Disapper");
            Destroy(this.gameObject);
        }
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
