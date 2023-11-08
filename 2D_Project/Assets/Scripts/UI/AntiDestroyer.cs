using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiDestroyer : MonoBehaviour
{
    //������Ʈ �ı� ������ ��ũ��Ʈ
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
