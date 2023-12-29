using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    // Start is called before the first frame update
    protected override void Start()
    {
        if (name.Equals("Slime"))
        {
            // 슬라임에 대한 속성 설정
            SetEnemyStatus("Slime", 100, 10, 2, 1.5f, 7f);
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

       // spriteRenderer.flipX = (dir < 0);

        // 기존의 Enemy 클래스의 MoveToTarget 메서드 호출
        base.MoveToTarget();

        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        enemyAnimator.SetInteger("WalkSpeed", (int)dir);
    }

}
