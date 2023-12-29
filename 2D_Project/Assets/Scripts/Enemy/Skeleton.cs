using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Start()
    {
        if (name.Equals("Skeleton"))
        {
            SetEnemyStatus("Skeleton", 1, 1, 0.5f, 1.5f, 7f);
        }

        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;

        //spriteRenderer.flipX = (dir < 0);
        // 기존의 Enemy 클래스의 MoveToTarget 메서드 호출
        base.MoveToTarget();

        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }

}
