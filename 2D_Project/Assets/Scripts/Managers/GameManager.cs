using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager   instance;

    //esc로 종료
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

    //씬이 바뀔때 딜레이 부여
    IEnumerator SceneChangeDelay() 
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("InGame");
    }
    //게임종료 딜레이 부여
    IEnumerator GameOffDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //씬 변경
    public void SceneChange()
    {
        StartCoroutine(SceneChangeDelay());
    }

    //X버튼 누르면 인게임 시간 정지
    public void ExitBtnClick()
    {
        Time.timeScale = 0;
    }

    //팝업 Yes버튼 누르면 나감
    public void ExitYesBtn()
    {
        StartCoroutine(GameOffDelay());
    }

    //팝업 No버튼 누르면 다시 게임재개
    public void ExitNoBtn()
    {
        Time.timeScale = 1f;
    }

    
}
