using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    private GameObject Archer;

    void Start()
    {
        Invoke("DestroyStone", 2);
    }

    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            if (raycast.collider.tag == "Archer")
            {
                Debug.Log("Hit Archer");
            }
            DestroyStone();
        }
        transform.Translate(transform.right * speed * Time.deltaTime);
    }


    // 돌을 파괴하는 메서드
    void DestroyStone()
    {
        Destroy(gameObject);
    }
}
