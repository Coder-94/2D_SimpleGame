using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 3;
    public Vector3 targetPosition; //대상 현재 위치

    private void Update()
    {
       if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x,
                               target.transform.position.y,
                               this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position,
                                                   targetPosition,
                                                   moveSpeed * Time.deltaTime);
        } 
    }
}
