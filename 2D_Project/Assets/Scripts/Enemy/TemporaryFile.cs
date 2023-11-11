using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryFile : MonoBehaviour
{
    Animator animator;

    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetInteger("walk", 0);
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetInteger("walk", 0);
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else animator.SetInteger("walk", 0); ;

        if (Input.GetKeyDown(KeyCode.A) &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("WizardAttack"))
        {
            animator.SetTrigger("Attack");
        }

    }
}
