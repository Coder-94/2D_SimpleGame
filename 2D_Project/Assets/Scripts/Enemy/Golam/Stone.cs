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

    // ���� �ð� �Ŀ� ���� �ı��ϴ� �ڷ�ƾ
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // ���� �÷��̾� �������� �̵���Ŵ
    void MoveStone()
    {
        if (Archer == null)
            return;

        Vector2 direction = (Archer.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // �浹�� Ȯ���ϰ� "Archer" �±׸� ���� ��ü�� �浹�ϸ� ���� �ı���
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

    // ���� �ı��ϴ� �޼���
    void DestroyStone()
    {
        Destroy(gameObject);
    }
}
