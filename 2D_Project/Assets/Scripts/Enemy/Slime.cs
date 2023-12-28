using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        spriteRenderer.flipX = (dir < 0);
        // ������ Enemy Ŭ������ MoveToTarget �޼��� ȣ��
        base.MoveToTarget();

        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }
}
