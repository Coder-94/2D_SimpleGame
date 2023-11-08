using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiDestroyer : MonoBehaviour
{
    //오브젝트 파괴 방지용 스크립트
    private static AntiDestroyer instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
