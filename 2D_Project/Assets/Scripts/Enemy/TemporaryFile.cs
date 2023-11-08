using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryFile : MonoBehaviour
{ 
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
