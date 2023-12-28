using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(2f));
    }

    void Update()
    {
        MoveStone();
        CheckCollision();
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void MoveStone()
    {
        transform.Translate(transform.right * -1f * speed * Time.deltaTime);
    }

    void CheckCollision()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            if (raycast.collider.CompareTag("Archer"))
            {
                Debug.Log("Hit Archer");
                DestroyStone();
            }
        }
    }

    void DestroyStone()
    {
        Destroy(gameObject);
    }
}
