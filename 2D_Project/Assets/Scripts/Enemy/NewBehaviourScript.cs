using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Enemy
{
    public float distance;
    public float atkdistance;
    public LayerMask isLayer;
    public float speed;

    public GameObject Stone;
    public Transform pos;

    public float cooltime;
    private float currenttime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if(raycast.collider != null)
        {

            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkdistance)
            {
                if(currenttime <= 0)
                {
                    GameObject Stonecopy = Instantiate(Stone, pos.position, transform.rotation);

                    currenttime = cooltime;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
