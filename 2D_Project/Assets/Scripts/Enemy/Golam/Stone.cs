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
        StartCoroutine(DestroyAfterDelay(2f));
    }

    void Update()
    {
        MoveStone();
        CheckCollision();
    }

    // 일정 시간 후에 돌을 파괴하는 코루틴
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // 돌을 플레이어 방향으로 이동시킴
    void MoveStone()
    {
        if (Archer == null)
            return;

        Vector2 direction = (Archer.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // 충돌을 확인하고 "Archer" 태그를 가진 객체와 충돌하면 돌을 파괴함
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

    // 돌을 파괴하는 메서드
    void DestroyStone()
    {
        Destroy(gameObject);
    }
}
