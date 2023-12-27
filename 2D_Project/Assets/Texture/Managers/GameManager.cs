using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager   instance;

    //esc�� ����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }

    //���� �ٲ� ������ �ο�
    IEnumerator SceneChangeDelay() 
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("InGame");
    }
    //�������� ������ �ο�
    IEnumerator GameOffDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //�� ����
    public void SceneChange()
    {
        StartCoroutine(SceneChangeDelay());
    }

    //X��ư ������ �ΰ��� �ð� ����
    public void ExitBtnClick()
    {
        Time.timeScale = 0;
    }

    //�˾� Yes��ư ������ ����
    public void ExitYesBtn()
    {
        StartCoroutine(GameOffDelay());
    }

    //�˾� No��ư ������ �ٽ� �����簳
    public void ExitNoBtn()
    {
        Time.timeScale = 1f;
    }

    
}
