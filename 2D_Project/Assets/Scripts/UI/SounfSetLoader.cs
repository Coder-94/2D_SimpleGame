using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SounfSetLoader : MonoBehaviour
{
    public Slider       bgm, master, se;

    // Start is called before the first frame update
    void Start()
    {
        float savedBGMValue = PlayerPrefs.GetFloat("BGMValue");
        bgm.value = savedBGMValue;
        float savedSEValue = PlayerPrefs.GetFloat("SEValue");
        se.value = savedSEValue;
        float savedMasterValue = PlayerPrefs.GetFloat("MasterValue");
        master.value = savedMasterValue;
    }
}
